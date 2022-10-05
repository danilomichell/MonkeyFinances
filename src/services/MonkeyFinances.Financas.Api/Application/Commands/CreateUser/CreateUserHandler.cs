using FluentValidation.Results;
using MediatR;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Data.Repositories;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Application.Commands.CreateUser
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
            var user = new User
            {
                Id = request.Id,
                Email = request.Email,
                Name = request.Nome
            };
            _userRepository.Adicionar(user);
            //user.AdicionarEvento(new User);
            return await PersistirDados(_userRepository.UnitOfWork);
        }
    }
}
