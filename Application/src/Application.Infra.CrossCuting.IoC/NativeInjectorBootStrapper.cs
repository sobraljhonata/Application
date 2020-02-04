using Application.Application.Interfaces;
using Application.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infra.CrossCuting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // Application
            services.AddScoped<IUsuarioServiceApp, UsuarioServiceApp>();

            // services.AddScoped<ApplicationDbContext>();
        }
    }
}