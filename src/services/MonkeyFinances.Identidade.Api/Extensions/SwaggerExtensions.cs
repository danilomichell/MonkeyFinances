using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models; 
using System.Reflection;
using MonkeyFinances.Identidade.Api.Extensions.Swagger;

namespace MonkeyFinances.Identidade.Api.Extensions
{
    /// <summary>
    /// Extensões do Swagger
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Configuração básica do SwaggerGen
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
                c.OperationFilter<BearerAuthenticationFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
            });
            services.AddFluentValidationRulesToSwagger();
            return services;
        }
    }
}
