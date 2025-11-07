using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Queries;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class GetTemplateByIdHandler: IRequestHandler<GetTemplateByIdQuery, Template?>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper mapper;

        public GetTemplateByIdHandler(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            this.mapper = mapper;
        }

        public async Task<Template?> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _templateRepository.GetTemplateById(request.Id, cancellationToken);
            return mapper.Map<Template?>(result);
        }
    }
}
