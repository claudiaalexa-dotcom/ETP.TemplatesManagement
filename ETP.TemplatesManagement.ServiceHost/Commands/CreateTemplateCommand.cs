using ETP.TemplatesManagement.SDK.DTOs;
using MediatR;


namespace ETP.TemplatesManagement.ServiceHost.Commands
{
    public class CreateTemplateCommand : IRequest<Template>
    {
        public TemplateBase Template { get; set; } = new TemplateBase();
    }
}
