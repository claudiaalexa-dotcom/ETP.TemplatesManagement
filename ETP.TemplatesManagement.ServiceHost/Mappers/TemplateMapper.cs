using AutoMapper;

namespace ETP.TemplatesManagement.ServiceHost.Mappers
{
    public class TemplateMapper: Profile
    {
        public TemplateMapper() 
        { 
            CreateMap<SDK.DTOs.Template, Data.Models.Template>().ReverseMap();
        }
    }

    public class TemplateBaseMapper : Profile
    {
        public TemplateBaseMapper()
        {
            CreateMap<SDK.DTOs.TemplateBase, Data.Models.Template>();
        }
    }
}
