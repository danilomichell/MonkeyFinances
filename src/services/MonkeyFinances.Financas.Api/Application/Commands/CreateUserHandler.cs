using FluentValidation.Results;
using MediatR;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Data.Repositories;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Application.Commands
{
    public class CreateUserHandler : CommandHandler,
        IRequestHandler<CreateUserCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ValidationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;
            _userRepository.Adicionar(new User
            {
                Id = request.Id,
                Email = request.Email,
                Name = request.Nome
            });
            return await PersistirDados(_userRepository.UnitOfWork);
        }
    }
}
