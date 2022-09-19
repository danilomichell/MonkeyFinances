using Microsoft.AspNetCore.Identity;
using MonkeyFinances.Identidade.Api.Data;

namespace MonkeyFinances.Identidade.Api.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityPortugueseErrors>();
            return services;
        }
    }
}
