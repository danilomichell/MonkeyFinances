using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonkeyFinances.Core.Controller;
using MonkeyFinances.Core.Exception;
using MonkeyFinances.Core.Mediator;
using MonkeyFinances.Financas.Api.Application.Commands.CreateUser;
using MonkeyFinances.Financas.Api.Models;

namespace MonkeyFinances.Financas.Api.Controllers;

[TypeFilter(typeof(ApiExceptionFilterAttribute))]
[Route("[controller]")]
public class UserController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public UserController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }
    [AllowAnonymous]
    [HttpPost("criar-usuario")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreate user)
    {
        var resultado = await _mediatorHandler.EnviarComando(
            new CreateUserCommand(new Guid(user.Id), user.Nome, user.Email));

        return CustomResponse(resultado);
    }
}