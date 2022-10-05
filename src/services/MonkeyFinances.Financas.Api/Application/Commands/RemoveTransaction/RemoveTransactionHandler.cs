using FluentValidation.Results;
using MediatR;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Data.Repositories;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Application.Commands.RemoveTransaction;

public class RemoveTransactionHandler : CommandHandler,
    IRequestHandler<RemoveTransactionCommand, ValidationResult>
{
    private readonly IUserRepository _userRepository;
    public RemoveTransactionHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ValidationResult> Handle(RemoveTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!request.EhValido()) return request.ValidationResult;
        var user = await _userRepository.ObterPorEmail(request.Email);
        if (user is null)
        {
            AdicionarErro("Não existe esse usuário cadastrado.");
            return ValidationResult;
        }

        if (user.Transacoes.Count == 0)
        {
            AdicionarErro("Não existe transações para esse usuário.");
            return ValidationResult;
        }

        var transacao = user.Transacoes.FirstOrDefault(x => x.Id == request.IdTransacao);
        if (transacao is null)
        {
            AdicionarErro("Não existe está transações cadastrada.");
            return ValidationResult;
        }

        _userRepository.RemoveTransacao(transacao);
        return await PersistirDados(_userRepository.UnitOfWork);
    }
}