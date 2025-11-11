using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Queries;
using MediatR;
using MongoDB.Bson;

namespace ETP.TemplatesManagement.ServiceHost.RequestHandlers
{
    public class GetTemplatesHandler : IRequestHandler<GetTemplatesQuery, List<Template>>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper mapper;

        public GetTemplatesHandler(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            this.mapper = mapper;
        }

        public async Task<List<Template>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var options = mapper.Map<Data.Models.SearchOptions>(request.SearchObject);
            var result = await _templateRepository.GetTemplatesAsync(options, cancellationToken);
            return mapper.Map<List<Template>>(result);
        }
    }
}
