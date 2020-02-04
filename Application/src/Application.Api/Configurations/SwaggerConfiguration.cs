using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Application.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Dominio.IO API",
                    Description = "API do site Dominio.IO",
                    TermsOfService = new Uri("http://site.dominio/terms"),
                    Contact = new OpenApiContact { Name = "Desenvolvedor X", Email = "email@dominio.com", Url = new Uri("http://dominio.com") },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("http://site.dominio/licensa") }
                });

                s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.ConfigureSwaggerGen(opt =>
            {
                opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }
    }
}