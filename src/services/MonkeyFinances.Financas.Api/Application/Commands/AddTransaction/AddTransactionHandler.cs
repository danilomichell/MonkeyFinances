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
        var transacao = new Transacao
        {
            IdUser = user.Id,
            IdTipo = tipo.Id,
            Valor = request.Valor,
            Descricao = request.Descricao,
            DataTransacao = request.DataTransacao,
            Parcela = new Parcela
            {
                IdFormaPagamento = formaPagamento.Id,
                NumParcela = request.NumParcela,
                TotalParcelas = request.TotalParcelas
            }
        };
        _userRepository.AdicionarTransacao(transacao);
        return await PersistirDados(_userRepository.UnitOfWork);
    }
}