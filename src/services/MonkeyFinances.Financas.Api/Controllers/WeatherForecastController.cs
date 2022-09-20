using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonkeyFinances.Core.Controller;
using MonkeyFinances.Core.Mediator;
using MonkeyFinances.Financas.Api.Application;
using MonkeyFinances.Financas.Api.Models;

namespace MonkeyFinances.Financas.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : MainController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IMediatorHandler _mediatorHandler;

        public WeatherForecastController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
        [AllowAnonymous]
        [HttpPost("Teste")]
        public async Task<IActionResult> Teste([FromBody] UserCreate user)
        {
            var resultado = await _mediatorHandler.EnviarComando(
                new CreateUserCommand(new Guid(user.Id), user.Nome, user.Email));

            return CustomResponse(resultado);
        }
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}