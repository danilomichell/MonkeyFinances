using FluentValidation;
using MonkeyFinances.Core.Messages;

namespace MonkeyFinances.Financas.Api.Application.Commands.CreateUser
{
    public class CreateUserCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public CreateUserCommand(Guid id, string nome, string email)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarClienteValidation : AbstractValidator<CreateUserCommand>
        {
            public RegistrarClienteValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do cliente não foi informado");
            }
        }
    }
}
