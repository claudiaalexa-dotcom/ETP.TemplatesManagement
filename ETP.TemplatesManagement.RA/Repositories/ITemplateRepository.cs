using ETP.TemplatesManagement.Data.Models;

namespace ETP.TemplatesManagement.RA.Repositories
{
    public interface ITemplateRepository
    {
        Task<Template> CreateTemplate(Template template, CancellationToken cancellationToken);
        Task<Template?> GetTemplateById(Guid id, CancellationToken cancellationToken);
        Task<List<Template>> GetTemplatesByAnchorPoint(AnchorPointSearchOptions anchorPoint, CancellationToken cancellationToken);
        Task<Template?> UpdateTemplate(Template updateTemplate, CancellationToken cancellationToken);
        Task<bool> DeleteTemplate(Guid id, CancellationToken cancellationToken);
        Task<List<Template>> GetTemplates(SearchOptions searchObject, CancellationToken cancellationToken);
    }
}
