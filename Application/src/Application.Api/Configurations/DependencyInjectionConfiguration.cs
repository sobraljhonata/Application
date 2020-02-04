using Application.Infra.CrossCuting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}