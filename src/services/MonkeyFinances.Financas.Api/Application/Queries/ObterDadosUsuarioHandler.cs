using FluentValidation.Results;
using MediatR;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Data.Repositories;

namespace MonkeyFinances.Financas.Api.Application.Queries;

public class ObterDadosUsuarioHandler : CommandHandler,
    IRequestHandler<ObterDadosUsuarioQuery, ValidationResult>
{
    private readonly IUserRepository _userRepository;
    public ObterDadosUsuarioHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ValidationResult> Handle(ObterDadosUsuarioQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.ObterPorEmail(request.Email);
        if (user is null)
        {
            return ValidationResult;
            //return new ObterDadosUsuarioResult();
        }

        return ValidationResult;
        //return new ObterDadosUsuarioResult
        //{
        //    Nome = user.Email,
        //    Email = user.Email
        //};
    }
}