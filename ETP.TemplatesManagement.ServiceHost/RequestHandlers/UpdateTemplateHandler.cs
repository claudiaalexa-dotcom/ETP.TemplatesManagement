using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Commands;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class UpdateTemplateHandler: IRequestHandler<UpdateTemplateCommand, Template?>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper mapper;

        public UpdateTemplateHandler(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            this.mapper = mapper;
        }

        public async Task<Template?> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<Data.Models.Template>(request.UpdateTemplate);
            model.Id = request.Id;
            var updated = await _templateRepository.UpdateTemplate(model, cancellationToken);
            return mapper.Map<Template>(updated);
        }
    }
}
