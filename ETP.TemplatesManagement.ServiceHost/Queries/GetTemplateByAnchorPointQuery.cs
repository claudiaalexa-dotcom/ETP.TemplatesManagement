using ETP.TemplatesManagement.SDK.DTOs;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Queries
{
    public class GetTemplateByAnchorPointQuery : IRequest<Template>
    {
        public AnchorPoint AnchorPoint { get; set; } = new AnchorPoint();
    }
}
