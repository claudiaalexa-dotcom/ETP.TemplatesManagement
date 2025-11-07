using ETP.TemplatesManagement.SDK.DTOs;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Commands
{
    public class UpdateTemplateCommand : IRequest<Template?>
    {
        public Guid Id { get; set; }
        public TemplateBase UpdateTemplate { get; set; } = new TemplateBase();
    }
}
