using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.ServiceHost.Commands;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class DeleteTemplateHandler: IRequestHandler<DeleteTemplateCommand, bool>
    {
        private readonly ITemplateRepository _templateRepository;

        public DeleteTemplateHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<bool> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _templateRepository.DeleteTemplate(request.Id, cancellationToken);
        }
    }
}
