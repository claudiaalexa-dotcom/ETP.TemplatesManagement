using System.Reflection;

namespace ETP.TemplatesManagement.ServiceHost.Extensions
{
    public static class AddAutoMapperService
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config => { }, Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
