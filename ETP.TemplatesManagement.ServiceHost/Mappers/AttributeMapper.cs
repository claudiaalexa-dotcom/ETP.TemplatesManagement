using AutoMapper;

namespace ETP.TemplatesManagement.ServiceHost.Mappers
{
    public class AttributeMapper : Profile
    {
        public AttributeMapper()
        {
            CreateMap<Data.Models.Attribute, SDK.DTOs.Attribute>()
                .ReverseMap();
        }
    }
}
