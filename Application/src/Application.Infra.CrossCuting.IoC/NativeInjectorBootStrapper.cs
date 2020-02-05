using Application.Application.Interfaces;
using Application.Application.Services;
using Application.Domain.Interface;
using Application.Infra.CrossCuting.Identity.Interfaces;
using Application.Infra.CrossCuting.Identity.Services;
using Application.Infra.Data.Context;
using Application.Infra.Data.Repository;
using Application.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infra.CrossCuting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            
            // Application
            services.AddScoped<IUsuarioServiceApp, UsuarioServiceApp>();

            // Infra - Data
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationContext>();
        }
    }
}