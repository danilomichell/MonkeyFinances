using Microsoft.EntityFrameworkCore;

namespace MonkeyFinances.Identidade.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
    }
}

