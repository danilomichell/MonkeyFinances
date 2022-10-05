using MediatR;
using MonkeyFinances.Core.Messages;

namespace MonkeyFinances.Financas.Api.Application.Queries
{
    public class ObterDadosUsuarioQuery : Command 
    {
        public string Email { get; set; }

        public ObterDadosUsuarioQuery(string email)
        {
            Email = email;
        }
    }
}
