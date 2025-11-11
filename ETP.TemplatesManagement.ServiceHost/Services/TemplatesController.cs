using ETP.TemplatesManagement.SDK.Contracts;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETP.TemplatesManagement.ServiceHost.Services
{
    [ApiController]
    [Route("api/templates")]
    public class TemplateController : ControllerBase, ITemplateService
    {
        private readonly IMediator mediator;

        public TemplateController(ILogger<TemplateController> logger, IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateTemplate")]
        public async Task<IActionResult> CreateAsync([FromBody] TemplateBase template, CancellationToken cancellationToken)
        {
            var createdTemplate = await mediator.Send(new CreateTemplateCommand() { Template = template }, cancellationToken);
            
            return Ok(createdTemplate);
        }

        [HttpPost("get-by-anchor-point", Name = "GetTemplateByAnchorPoint")]
        public async Task<IActionResult> GetByAnchorPointAsync([FromBody] AnchorPointSearchOptions searchOptions, CancellationToken cancellationToken)
        {
            var template = await mediator.Send(new GetTemplatesByAnchorPointQuery() { SearchOptions = searchOptions }, cancellationToken);
            return Ok(template);
        }

        [HttpGet("{id}", Name = "GetTemplateById")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var template = await mediator.Send(new GetTemplateByIdQuery() { Id = id }, cancellationToken);
            return Ok(template);
        }

        [HttpGet(Name = "GetAllTemplates")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int? page, [FromQuery] int? count, [FromQuery] string? searchTerm, [FromQuery] string? sortColumn, [FromQuery] string? sortOrder, CancellationToken cancellationToken)
        {
            var searchObject = new SearchOptions()
            {
                Page = page,
                Count = count,
                SearchTerm = searchTerm,
                SortColumn = sortColumn,
                SortOrder = sortOrder
            };

            var templates = await mediator.Send(new GetTemplatesQuery() { SearchObject = searchObject }, cancellationToken);
            return Ok(templates);
        }

        [HttpPut("{id}", Name = "UpdateTemplate")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] TemplateBase updateTemplate, CancellationToken cancellationToken)
        {
            var updatedTemplate = await mediator.Send(new UpdateTemplateCommand() { Id = id, UpdateTemplate = updateTemplate }, cancellationToken);
            return Ok(updatedTemplate);
        }

        [HttpDelete("{id}", Name = "DeleteTemplate")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteTemplateCommand() { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}
