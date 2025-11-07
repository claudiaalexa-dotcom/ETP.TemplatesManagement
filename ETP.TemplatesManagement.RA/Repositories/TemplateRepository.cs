using ETP.TemplatesManagement.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace ETP.TemplatesManagement.RA.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        public static readonly int DefaultPaginationCount = 2;

        //private List<Template> templates = JsonSerializer.Deserialize<List<Template>>(File.ReadAllText("./Repository/templates.json")) ?? new List<Template>();

        private readonly IMongoCollection<Template> collection;

        public TemplateRepository(IMongoCollection<Template> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<Template> CreateTemplate(Template templateBase, CancellationToken cancellationToken)
        {
            var template = new Template
            {
                Id = Guid.NewGuid(),
                AnchorPoint = templateBase.AnchorPoint,
                Title = templateBase.Title,
                Attributes = templateBase.Attributes
            };

            await collection.InsertOneAsync(template, null, cancellationToken);

            return template;
        }

        public async Task<Template?> GetTemplateByAnchorPoint(AnchorPoint anchorPoint, CancellationToken cancellationToken)
        {
            var filter = Builders<Template>.Filter.And(
                Builders<Template>.Filter.Eq(t => t.AnchorPoint.DeliveryOwner.Id, anchorPoint.DeliveryOwner.Id),
                Builders<Template>.Filter.Eq(t => t.AnchorPoint.ServiceLine.Id, anchorPoint.ServiceLine.Id),
                Builders<Template>.Filter.Eq(t => t.AnchorPoint.MarketOffering.Id, anchorPoint.MarketOffering.Id)
            );

            return await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Template?> GetTemplateById(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<Template>.Filter.Eq(t => t.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Template?> UpdateTemplate(Template updateTemplate, CancellationToken cancellationToken)
        {
            var template = GetTemplateById(updateTemplate.Id, cancellationToken);
            if (template == null)
            {
                return null;
            }

            var filter = Builders<Template>.Filter.Eq(t => t.Id, updateTemplate.Id);

            // Replace and return the updated document
            var options = new FindOneAndReplaceOptions<Template>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = false
            };

            return await collection.FindOneAndReplaceAsync(filter, updateTemplate, options, cancellationToken);
        }

        public async Task<bool> DeleteTemplate(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<Template>.Filter.Eq(t => t.Id, id);
            var result = await collection.DeleteOneAsync(filter, cancellationToken);
            return result.DeletedCount > 0;
        }

        public async Task<List<Template>> GetTemplates(SearchOptions searchObject, CancellationToken cancellationToken)
        {
            var filter = Builders<Template>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(searchObject?.SearchTerm))
            {
                var term = Regex.Escape(searchObject!.SearchTerm!.Trim());
                var regex = new BsonRegularExpression(term, "i");

                var titleFilter = Builders<Template>.Filter.Regex(t => t.Title, regex);
                var deliveryOwnerFilter = Builders<Template>.Filter.Regex("AnchorPoint.DeliveryOwnerName", regex);
                var serviceLineFilter = Builders<Template>.Filter.Regex("AnchorPoint.ServiceLineName", regex);
                var marketOfferingFilter = Builders<Template>.Filter.Regex("AnchorPoint.MarketOfferingName", regex);
                var attributeNameFilter = Builders<Template>.Filter.ElemMatch(t => t.Attributes,
                    Builders<Data.Models.Attribute>.Filter.Regex(a => a.Name, regex));
                var attributeDescriptionFilter = Builders<Template>.Filter.ElemMatch(t => t.Attributes,
                    Builders<Data.Models.Attribute>.Filter.Regex(a => a.Description, regex));

                filter = Builders<Template>.Filter.Or(
                    titleFilter,
                    deliveryOwnerFilter,
                    serviceLineFilter,
                    marketOfferingFilter,
                    attributeNameFilter,
                    attributeDescriptionFilter
                );
            }

            // Sorting
            var sortColumn = (searchObject?.SortColumn ?? "title").Trim().ToLowerInvariant();
            var sortOrder = (searchObject?.SortOrder ?? "asc").Trim().ToLowerInvariant();

            SortDefinition<Template> sort = sortColumn switch
            {
                "deliveryowner" or "deliveryownername" => sortOrder == "desc"
                    ? Builders<Template>.Sort.Descending("AnchorPoint.DeliveryOwnerName")
                    : Builders<Template>.Sort.Ascending("AnchorPoint.DeliveryOwnerName"),

                "serviceline" or "servicelinename" => sortOrder == "desc"
                    ? Builders<Template>.Sort.Descending("AnchorPoint.ServiceLineName")
                    : Builders<Template>.Sort.Ascending("AnchorPoint.ServiceLineName"),

                "marketoffering" or "marketofferingname" => sortOrder == "desc"
                    ? Builders<Template>.Sort.Descending("AnchorPoint.MarketOfferingName")
                    : Builders<Template>.Sort.Ascending("AnchorPoint.MarketOfferingName"),

                _ => sortOrder == "desc"
                    ? Builders<Template>.Sort.Descending(t => t.Title)
                    : Builders<Template>.Sort.Ascending(t => t.Title),
            };

            // Pagination
            int page = searchObject?.Page ?? 1;
            int count = searchObject?.Count ?? DefaultPaginationCount;
            if (page < 1) page = 1;
            if (count < 1) count = DefaultPaginationCount;

            var find = collection.Find(filter).Sort(sort).Skip((page - 1) * count).Limit(count);
            return await find.ToListAsync(cancellationToken);
        }
    }
}
