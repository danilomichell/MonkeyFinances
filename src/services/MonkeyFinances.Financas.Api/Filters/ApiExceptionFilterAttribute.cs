using System.Net;
using System.Text;
using System.Text.Json;
using FluentValidation; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MonkeyFinances.Financas.Api.Extensions;

namespace MonkeyFinances.Financas.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException:
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    context.HttpContext.Response.Headers.Clear();
                    var mensagem = new StringBuilder();
                    foreach (var validationsfailures in (context.Exception as ValidationException)!.Errors!)
                        mensagem.Append($"- {validationsfailures.ErrorMessage}{Environment.NewLine}");
                    context.Result = new ContentResult
                    {
                        Content = "{\r\n  \"title\": \"One or more validation errors occurred.\",\r\n  " +
                                  $"\"status\": {(int)HttpStatusCode.BadRequest},\r\n  " +
                                  "\"errors\": {\r\n    " +
                                  "\"Mensagens\": [\r\n      " +
                                  $"\"{mensagem}\"\r\n    ]\r\n  " +
                                  "}\r\n}", 
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        ContentType = "application/json"
                    };
                    return;
                }
                case InvalidOperationException: 
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.HttpContext.Response.Headers.Clear();
                    context.Result = new ContentResult
                    {
                        Content = "{\r\n  \"title\": \"One or more validation errors occurred.\",\r\n  " +
                                  $"\"status\": {(int)HttpStatusCode.NotFound},\r\n  " +
                                  "\"errors\": {\r\n    " +
                                  "\"Mensagens\": [\r\n      " +
                                  $"\"{context.Exception.Message}\"\r\n    ]\r\n  " +
                                  "}\r\n}",
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ContentType = "application/json"
                    };
                    return;
                case ArgumentNullException or ArgumentOutOfRangeException or ArgumentException
                    or OperationCanceledException:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.HttpContext.Response.Headers.Clear();
                    context.Result = new ContentResult
                    {
                        Content = "{\r\n  \"title\": \"One or more validation errors occurred.\",\r\n  " +
                                  $"\"status\": {(int)HttpStatusCode.BadRequest},\r\n  " +
                                  "\"errors\": {\r\n    " +
                                  "\"Mensagens\": [\r\n      " +
                                  $"\"{context.Exception.Message}\"\r\n    ]\r\n  " +
                                  "}\r\n}", 
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        ContentType = "application/json"
                    };
                    return;
                case NotImplementedException:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    context.HttpContext.Response.Headers.Clear();
                    context.Result = new ContentResult
                    {
                        Content = "{\r\n  \"title\": \"One or more validation errors occurred.\",\r\n  " +
                                  $"\"status\": {(int)HttpStatusCode.NotImplemented},\r\n  " +
                                  "\"errors\": {\r\n    " +
                                  "\"Mensagens\": [\r\n      " +
                                  $"\"Funcionalidade ainda não implementada.\"\r\n    ]\r\n  " +
                                  "}\r\n}", 
                        StatusCode = (int)HttpStatusCode.NotImplemented,
                        ContentType = "application/json"
                    };
                    return;
                case DbUpdateException:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    context.HttpContext.Response.Headers.Clear();
                    context.Result = new ContentResult
                    {
                        Content = "{\r\n  \"title\": \"One or more validation errors occurred.\",\r\n  " +
                                  $"\"status\": {(int)HttpStatusCode.InternalServerError},\r\n  " +
                                  "\"errors\": {\r\n    " +
                                  "\"Mensagens\": [\r\n      " +
                                  $"\"{context.Exception.GetAllMessagesAsString()}\"\r\n    ]\r\n  " +
                                  "}\r\n}", 
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        ContentType = "application/json"
                    };
                    return;
                case JsonException:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.HttpContext.Response.Headers.Clear();
                    context.Result = new ContentResult
                    {
                        Content = "{\r\n  \"title\": \"One or more validation errors occurred.\",\r\n  " +
                                  $"\"status\": {(int)HttpStatusCode.InternalServerError},\r\n  " +
                                  "\"errors\": {\r\n    " +
                                  "\"Mensagens\": [\r\n      " +
                                  $"\"{context.Exception.Message}\"\r\n    ]\r\n  " +
                                  "}\r\n}", 
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        ContentType = "application/json"
                    };
                    return;
            }

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.Headers.Clear();
            context.Result = new ContentResult
            {
                Content = "{\r\n  \"title\": \"One or more validation errors occurred.\",\r\n  " +
                          $"\"status\": {(int)HttpStatusCode.InternalServerError},\r\n  " +
                          "\"errors\": {\r\n    " +
                          "\"Mensagens\": [\r\n      " +
                          $"\"{context.Exception.Message}\"\r\n    ]\r\n  " +
                          "}\r\n}", 
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ContentType = "application/json"
            };

            _logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}
