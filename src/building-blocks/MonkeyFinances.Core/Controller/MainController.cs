﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace MonkeyFinances.Core.Controller
{
    [ApiController]
    public abstract class MainController : Microsoft.AspNetCore.Mvc.Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object? result = null)
        {
            if (!OperacaoValida())
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Mensagens", Erros.ToArray() }
                }));
            if (result == null) return Ok();
            return Ok(result);

        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
