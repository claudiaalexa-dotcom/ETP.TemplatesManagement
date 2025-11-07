using AutoMapper;

namespace ETP.TemplatesManagement.ServiceHost.Mappers
{
    public class SearchOptionsMapper: Profile
    {
        public SearchOptionsMapper() {
            CreateMap<SDK.DTOs.SearchOptions, Data.Models.SearchOptions>().ReverseMap();
        }
    }
}
