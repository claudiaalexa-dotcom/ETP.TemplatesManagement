using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Queries;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class GetTemplateByAnchorPointHandler: IRequestHandler<GetTemplateByAnchorPointQuery, Template?>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper mapper;

        public GetTemplateByAnchorPointHandler(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            this.mapper = mapper;
        }

        public async Task<Template?> Handle(GetTemplateByAnchorPointQuery request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<Data.Models.AnchorPoint>(request.AnchorPoint);
            var result = await _templateRepository.GetTemplateByAnchorPoint(model, cancellationToken);
            return mapper.Map<Template?>(result);
        }
    }
}
