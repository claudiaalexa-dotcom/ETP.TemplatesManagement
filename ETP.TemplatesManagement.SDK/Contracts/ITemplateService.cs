using ETP.TemplatesManagement.SDK.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace ETP.TemplatesManagement.SDK.Contracts
{
    public interface ITemplateService
    {
        Task<IActionResult> CreateAsync([FromBody] TemplateBase template, CancellationToken cancellationToken);
        Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken);
        Task<IActionResult> GetAllAsync([FromQuery] int? page, [FromQuery] int? count, [FromQuery] string? searchTerm, [FromQuery] string? sortColumn, [FromQuery] string? sortOrder, CancellationToken cancellationToken);
        Task<IActionResult> GetByAnchorPointAsync([FromBody] AnchorPointSearchOptions searchOptions, CancellationToken cancellationToken);
        Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken);
        Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] TemplateBase updateTemplate, CancellationToken cancellationToken);
    }
}
