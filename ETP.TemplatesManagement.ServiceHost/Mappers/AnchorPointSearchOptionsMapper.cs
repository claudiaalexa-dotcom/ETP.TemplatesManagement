using AutoMapper;
using ETP.TemplatesManagement.Data.Models;

namespace ETP.TemplatesManagement.ServiceHost.Mappers
{
    public class AnchorPointSearchOptionsMapper: Profile
    {
        public AnchorPointSearchOptionsMapper()
        {
            CreateMap<SDK.DTOs.AnchorPointSearchOptions, AnchorPointSearchOptions>()
                .ReverseMap();
        }
    }
}
