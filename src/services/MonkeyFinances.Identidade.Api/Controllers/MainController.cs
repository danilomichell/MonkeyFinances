using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MonkeyFinances.Identidade.Api.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object? result = null)
        {
            if (ValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddErrors(error.ErrorMessage);
            }

            return CustomResponse();
        }
        protected bool ValidOperation()
        {
            return !Erros.Any();
        }

        protected void AddErrors(string erro)
        {
            Erros.Add(erro);
        }
        protected void ClearErrors()
        {
            Erros.Clear();
        }
    }
}
