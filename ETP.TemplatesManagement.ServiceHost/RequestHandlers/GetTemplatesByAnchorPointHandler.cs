using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Queries;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class GetTemplatesByAnchorPointHandler: IRequestHandler<GetTemplatesByAnchorPointQuery, List<Template>>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper mapper;

        public GetTemplatesByAnchorPointHandler(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            this.mapper = mapper;
        }

        public async Task<List<Template>> Handle(GetTemplatesByAnchorPointQuery request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<Data.Models.AnchorPointSearchOptions>(request.SearchOptions);
            var result = await _templateRepository.GetTemplatesByAnchorPoint(model, cancellationToken);
            
            return mapper.Map<List<Template>>(result);
        }
    }
}
