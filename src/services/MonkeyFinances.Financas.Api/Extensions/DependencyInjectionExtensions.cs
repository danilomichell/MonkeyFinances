using Microsoft.EntityFrameworkCore;
using MonkeyFinances.Financas.Api.Data;
using MonkeyFinances.Financas.Api.Filters;

namespace MonkeyFinances.Financas.Api.Extensions
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
            services.AddDbContext<FinancasContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FinancasContext")));
            return services;
        }
        public static IServiceCollection AddServiceDependencyInjection(this IServiceCollection services)
        { 
            services.AddScoped<ApiExceptionFilterAttribute>();
            return services;
        }
    }
}
