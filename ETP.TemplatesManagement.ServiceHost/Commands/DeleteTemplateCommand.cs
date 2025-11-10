using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Commands
{
    public class DeleteTemplateCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
