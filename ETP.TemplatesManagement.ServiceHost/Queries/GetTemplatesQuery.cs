using ETP.TemplatesManagement.SDK.DTOs;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Queries
{
    public class GetTemplatesQuery : IRequest<List<Template>>
    {
        public SearchOptions SearchObject { get; set; } = new SearchOptions();
    }
}
