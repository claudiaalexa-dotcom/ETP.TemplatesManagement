using AutoMapper;

namespace ETP.TemplatesManagement.ServiceHost.Mappers
{
    public class AnchorPointMapper: Profile
    {
        public AnchorPointMapper() {
            CreateMap<SDK.DTOs.AnchorPoint, Data.Models.AnchorPoint>()
                .ForMember(dest => dest.DeliveryOwner, opt => opt.MapFrom(src => new Data.Models.DeliveryOwner(src.DeliveryOwner.Id, src.DeliveryOwner.Name)))
                .ForMember(dest => dest.ServiceLine, opt => opt.MapFrom(src => new Data.Models.ServiceLine(src.ServiceLine.Id, src.ServiceLine.Name)))
                .ForMember(dest => dest.MarketOffering, opt => opt.MapFrom(src => new Data.Models.MarketOffering(src.MarketOffering.Id, src.MarketOffering.Name)))
                .ReverseMap()
                .ForMember(dest => dest.DeliveryOwner, opt => opt.MapFrom(src => new SDK.DTOs.DeliveryOwner(src.DeliveryOwner.Id, src.DeliveryOwner.Name)))
                .ForMember(dest => dest.ServiceLine, opt => opt.MapFrom(src => new SDK.DTOs.ServiceLine(src.ServiceLine.Id, src.ServiceLine.Name)))
                .ForMember(dest => dest.MarketOffering, opt => opt.MapFrom(src => new SDK.DTOs.MarketOffering(src.MarketOffering.Id, src.MarketOffering.Name)));
        }
    }
}
