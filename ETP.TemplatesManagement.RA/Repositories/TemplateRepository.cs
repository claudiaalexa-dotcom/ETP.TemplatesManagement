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

        public async Task<List<Template>> GetTemplatesByAnchorPoint(AnchorPointSearchOptions searchOptions, CancellationToken cancellationToken)
        {
            if (searchOptions == null)
            {
                return await collection.Find(Builders<Template>.Filter.Empty).ToListAsync(cancellationToken);
            }

            var filters = new List<FilterDefinition<Template>>();

            if (searchOptions.DeliveryOwnerIds != null && searchOptions.DeliveryOwnerIds.Any())
            {
                filters.Add(Builders<Template>.Filter.In(t => t.AnchorPoint.DeliveryOwnerId, searchOptions.DeliveryOwnerIds));
            }

            if (searchOptions.DeliveryOwnerNames != null && searchOptions.DeliveryOwnerNames.Any())
            {
                filters.Add(Builders<Template>.Filter.In("AnchorPoint.DeliveryOwnerName", searchOptions.DeliveryOwnerNames));
            }

            if (searchOptions.ServiceLineIds != null && searchOptions.ServiceLineIds.Any())
            {
                filters.Add(Builders<Template>.Filter.In(t => t.AnchorPoint.ServiceLineId, searchOptions.ServiceLineIds));
            }

            if (searchOptions.ServiceLineNames != null && searchOptions.ServiceLineNames.Any())
            {
                filters.Add(Builders<Template>.Filter.In("AnchorPoint.ServiceLineName", searchOptions.ServiceLineNames));
            }

            if (searchOptions.MarketOfferingIds != null && searchOptions.MarketOfferingIds.Any())
            {
                filters.Add(Builders<Template>.Filter.In(t => t.AnchorPoint.MarketOfferingId, searchOptions.MarketOfferingIds));
            }

            if (searchOptions.MarketOfferingNames != null && searchOptions.MarketOfferingNames.Any())
            {
                filters.Add(Builders<Template>.Filter.In("AnchorPoint.MarketOfferingName", searchOptions.MarketOfferingNames));
            }

            var filter = filters.Count > 0
                ? Builders<Template>.Filter.And(filters)
                : Builders<Template>.Filter.Empty;

            return await collection.Find(filter).ToListAsync(cancellationToken);
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

        public async Task<List<Template>> GetTemplates(SearchOptions searchOptions, CancellationToken cancellationToken)
        {
            var filter = Builders<Template>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(searchOptions?.SearchTerm))
            {
                var term = Regex.Escape(searchOptions!.SearchTerm!.Trim());
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
            var sortColumn = (searchOptions?.SortColumn ?? "title").Trim().ToLowerInvariant();
            var sortOrder = (searchOptions?.SortOrder ?? "asc").Trim().ToLowerInvariant();

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
            int page = searchOptions?.Page ?? 1;
            int count = searchOptions?.Count ?? DefaultPaginationCount;
            if (page < 1) page = 1;
            if (count < 1) count = DefaultPaginationCount;

            var find = collection.Find(filter).Sort(sort).Skip((page - 1) * count).Limit(count);
            return await find.ToListAsync(cancellationToken);
        }
    }
}
