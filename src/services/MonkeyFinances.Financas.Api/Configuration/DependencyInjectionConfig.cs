﻿using FluentValidation.Results;
using MediatR;
using MonkeyFinances.Core.Mediator;
using MonkeyFinances.Financas.Api.Application.Commands;
using MonkeyFinances.Financas.Api.Data;
using MonkeyFinances.Financas.Api.Data.Repositories;

namespace MonkeyFinances.Financas.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, ValidationResult>, CreateUserHandler>();

            //services.AddScoped<INotificationHandler<CreateUserEvent>, ClienteEventHandler>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<FinancasContext>();
        }
    }
}
