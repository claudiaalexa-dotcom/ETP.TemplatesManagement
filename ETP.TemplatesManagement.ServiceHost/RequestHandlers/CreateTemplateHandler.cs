using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Commands;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class CreateTemplateHandler: IRequestHandler<CreateTemplateCommand, Template>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper mapper;

        public CreateTemplateHandler(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            this.mapper = mapper;
        }

        public async Task<Template> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<Data.Models.Template>(request.Template);
            var created = await _templateRepository.CreateTemplate(model, cancellationToken);
            return mapper.Map<Template>(created);
        }
    }
}
