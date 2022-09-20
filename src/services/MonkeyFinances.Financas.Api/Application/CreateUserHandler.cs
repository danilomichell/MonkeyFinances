using FluentValidation.Results;
using MediatR;
using MonkeyFinances.Core.Messages;

namespace MonkeyFinances.Financas.Api.Application
{
    public class CreateUserHandler : CommandHandler,
        IRequestHandler<CreateUserCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;
            AdicionarErro("Ainda não foi implementado.");
            return ValidationResult;
            //return await PersistirDados(.UnitOfWork);
            return request.ValidationResult;
        }
    }
}
