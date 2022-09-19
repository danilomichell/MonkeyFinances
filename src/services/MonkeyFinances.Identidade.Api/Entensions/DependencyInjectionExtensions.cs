using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonkeyFinances.Identidade.Api.Data;
using MonkeyFinances.Identidade.Api.Filters;
using MonkeyFinances.Identidade.Api.Services;

namespace MonkeyFinances.Identidade.Api.Entensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddGeneralSettings(this IServiceCollection services)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            return services;
        }
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentidadeConnection")));
            return services;
        }
        public static IServiceCollection AddServiceDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ApiExceptionFilterAttribute>();
            return services;
        }
    }
}
