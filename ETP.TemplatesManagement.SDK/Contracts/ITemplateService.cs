using ETP.TemplatesManagement.SDK.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace ETP.TemplatesManagement.SDK.Contracts
{
    public interface ITemplateService
    {
        Task<IActionResult> Create([FromBody] TemplateBase template, CancellationToken cancellationToken);
        Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken);
        Task<IActionResult> GetAll([FromQuery] int? page, [FromQuery] int? count, [FromQuery] string? searchTerm, [FromQuery] string? sortColumn, [FromQuery] string? sortOrder, CancellationToken cancellationToken);
        Task<IActionResult> GetByAnchorPoint([FromBody] AnchorPoint anchorPoint, CancellationToken cancellationToken);
        Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken);
        Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TemplateBase updateTemplate, CancellationToken cancellationToken);
    }
}
