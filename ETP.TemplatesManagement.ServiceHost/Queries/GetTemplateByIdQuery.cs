using ETP.TemplatesManagement.SDK.DTOs;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Queries
{
    public class GetTemplateByIdQuery : IRequest<Template>
    {
        public Guid Id { get; set; }
    }
}
