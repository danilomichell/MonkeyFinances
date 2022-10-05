using FluentValidation;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Models.Enuns;

namespace MonkeyFinances.Financas.Api.Application.Commands.RemoveTransaction;

public class RemoveTransactionCommand : Command
{
    public string Email { get; set; }
    public Guid IdTransacao { get; set; }

    public RemoveTransactionCommand(string email, Guid idTransacao)
    {
        Email = email;
        IdTransacao = idTransacao;
    }
    public override bool EhValido()
    {
        ValidationResult = new RegistrarClienteValidation().Validate(this);
        return ValidationResult.IsValid;
    }
    public class RegistrarClienteValidation : AbstractValidator<RemoveTransactionCommand>
    {
        public RegistrarClienteValidation()
        {
        }
    }
}