using ETP.TemplatesManagement.Data.Models;

namespace ETP.TemplatesManagement.RA.Repositories
{
    public interface ITemplateRepository
    {
        Task<Template> CreateTemplateAsync(Template template, CancellationToken cancellationToken);
        Task<Template?> GetTemplateByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Template>> GetTemplatesByAnchorPointAsync(AnchorPointSearchOptions anchorPoint, CancellationToken cancellationToken);
        Task<Template?> UpdateTemplateAsync(Template updateTemplate, CancellationToken cancellationToken);
        Task<bool> DeleteTemplateAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Template>> GetTemplatesAsync(SearchOptions searchObject, CancellationToken cancellationToken);
    }
}
