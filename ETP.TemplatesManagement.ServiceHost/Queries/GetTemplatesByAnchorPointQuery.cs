using ETP.TemplatesManagement.SDK.DTOs;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Queries
{
    public class GetTemplatesByAnchorPointQuery : IRequest<List<Template>>
    {
        public AnchorPointSearchOptions SearchOptions { get; set; } = new AnchorPointSearchOptions();
    }
}
