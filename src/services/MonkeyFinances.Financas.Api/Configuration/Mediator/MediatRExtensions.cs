using FluentValidation;
using MediatR;  

namespace MonkeyFinances.Financas.Api.Configuration.Mediator
{
    public static class MediatRExtensions
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("MonkeyFinances.Financas.Api");
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
