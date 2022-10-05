using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonkeyFinances.Core.Controller;
using MonkeyFinances.Core.Mediator;
using MonkeyFinances.Financas.Api.Application.Commands.AddTransaction;
using MonkeyFinances.Financas.Api.Models;

namespace MonkeyFinances.Financas.Api.Controllers;

public class TransactionController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public TransactionController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }
    [AllowAnonymous]
    [HttpPut("adicionar-transacao")]
    public async Task<IActionResult> AddTransaction([FromBody] AddTransaction transaction)
    {
        var resultado = await _mediatorHandler.EnviarComando(
            new AddTransactionCommand(transaction.Email, transaction.Descricao, transaction.DataTransacao, transaction.Valor,
                transaction.Tipo, transaction.NumParcela, transaction.TotalParcelas, transaction.FormaPagamento));

        return CustomResponse(true,resultado);
    }

}