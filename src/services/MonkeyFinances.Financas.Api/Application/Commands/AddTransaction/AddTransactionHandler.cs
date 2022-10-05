using FluentValidation.Results;
using MediatR;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Data.Repositories;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Application.Commands.AddTransaction;

public class AddTransactionHandler : CommandHandler,
    IRequestHandler<AddTransactionCommand, ValidationResult>
{
    private readonly IUserRepository _userRepository;
    public AddTransactionHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ValidationResult> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!request.EhValido()) return request.ValidationResult;
        var user = await _userRepository.ObterPorEmail(request.Email);
        if (user is null)
        {
            AdicionarErro("Não existe esse usuário cadastrado.");
            return ValidationResult;
        }
        var tipo = await _userRepository.ObterTipos(request.Tipo);
        if (tipo is null)
        {
            AdicionarErro("Não existe esse tipo cadastrado");
            return ValidationResult;
        }
        var formaPagamento = await _userRepository.ObterFormaPagamento(request.FormaPagamento);
        if (formaPagamento is null)
        {
            AdicionarErro("Não existe essa forma de pagamento cadastrado");
            return ValidationResult;
        }
        _userRepository.AdicionarTransacao(new Transacao
        (
            descricao: request.Descricao,
            tipo: tipo,
            valor: request.Valor,
            dataTransacao: request.DataTransacao,
            user: user,
            numParcela: request.NumParcela,
            totalParcelas: request.TotalParcelas,
            formaPagamento: formaPagamento
        ));

        return await PersistirDados(_userRepository.UnitOfWork);
    }
}