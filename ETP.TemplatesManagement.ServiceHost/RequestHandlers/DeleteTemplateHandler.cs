using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.ServiceHost.Commands;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class DeleteTemplateHandler: IRequestHandler<DeleteTemplateCommand>
    {
        private readonly ITemplateRepository _templateRepository;

        public DeleteTemplateHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var found = await _templateRepository.DeleteTemplateAsync(request.Id, cancellationToken);
            if (!found)
            {
                throw new KeyNotFoundException($"Template with Id {request.Id} not found.");
            }
        }
    }
}
