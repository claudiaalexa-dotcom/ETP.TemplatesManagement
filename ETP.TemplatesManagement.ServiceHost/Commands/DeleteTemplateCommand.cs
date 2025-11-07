using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Commands
{
    public class DeleteTemplateCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
