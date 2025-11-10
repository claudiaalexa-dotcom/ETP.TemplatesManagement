using AutoMapper;

namespace ETP.TemplatesManagement.ServiceHost.Mappers
{
    public class AnchorPointMapper : Profile
    {
        public AnchorPointMapper()
        {
            CreateMap<SDK.DTOs.AnchorPoint, Data.Models.AnchorPoint>()
                .ReverseMap();
        }
    }
}
