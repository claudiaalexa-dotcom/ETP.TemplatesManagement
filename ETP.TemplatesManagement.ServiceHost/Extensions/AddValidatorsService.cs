using ETP.TemplatesManagement.ServiceHost.Behaviors;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace ETP.TemplatesManagement.ServiceHost.Extensions
{
    public static class AddValidatorsService
    {
        public static IServiceCollection AddValidationAndMediatR(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
