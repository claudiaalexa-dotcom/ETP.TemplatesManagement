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
    public class TemplateController : BaseApiController, ITemplateService
    {
        private readonly IMediator mediator;

        public TemplateController(ILogger<TemplateController> logger, IMediator mediator) : base(logger)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateTemplate")]
        public Task<IActionResult> Create([FromBody] TemplateBase template, CancellationToken cancellationToken)
        {
            return ExecuteApiCall(async () =>
            {
                var createdTemplate = await mediator.Send(new CreateTemplateCommand() { Template = template }, cancellationToken);
                return Ok(createdTemplate);
            },
            "Error creating template");
        }

        [HttpPost("get-by-anchor-point", Name = "GetTemplateByAnchorPoint")]
        public Task<IActionResult> GetByAnchorPoint([FromBody] AnchorPoint anchorPoint, CancellationToken cancellationToken)
        {
            return ExecuteApiCall(async () =>
            {
                var template = await mediator.Send(new GetTemplateByAnchorPointQuery() { AnchorPoint = anchorPoint }, cancellationToken);
                return template != null
                     ? Ok(template)
                     : NotFound();
            },
            "Error retrieving template by anchor point");
        }

        [HttpGet("{id}", Name = "GetTemplateById")]
        public Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return ExecuteApiCall(async () =>
            {
                var template = await mediator.Send(new GetTemplateByIdQuery() { Id = id }, cancellationToken);
                return template != null
                     ? Ok(template)
                     : NotFound();
            },
            "Error retrieving template by ID");
        }

        [HttpGet(Name = "GetAllTemplates")]
        public Task<IActionResult> GetAll([FromQuery] int? page, [FromQuery] int? count, [FromQuery] string? searchTerm, [FromQuery] string? sortColumn, [FromQuery] string? sortOrder, CancellationToken cancellationToken)
        {
            return ExecuteApiCall(async () =>
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
            },
            "Error retrieving template by ID");
        }

        [HttpPut("{id}", Name = "UpdateTemplate")]
        public Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TemplateBase updateTemplate, CancellationToken cancellationToken)
        {
            return ExecuteApiCall(async () =>
            {
                var updatedTemplate = await mediator.Send(new UpdateTemplateCommand() { Id = id, UpdateTemplate = updateTemplate }, cancellationToken);
                return updatedTemplate != null
                     ? Ok(updatedTemplate)
                     : NotFound();
            },
            "Error updating template");
        }

        [HttpDelete("{id}", Name = "DeleteTemplate")]
        public Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return ExecuteApiCall(async () =>
            {
                var deleted = await mediator.Send(new DeleteTemplateCommand() { Id = id }, cancellationToken);
                return deleted
                     ? NoContent()
                     : NotFound();
            },
            "Error deleting template");
        }
    }
}
