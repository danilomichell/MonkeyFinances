using MonkeyFinances.Core.DomainObjects;

namespace MonkeyFinances.Financas.Api.Models
{
    public class User : Entity, IAggregateRoot
    {
        public string Nome { get; private set; } = null!;
        public string Email { get; set; } = null!;
        public bool Deleted { get; private set; }

        protected User() { }
        public User(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Deleted = false;
        }
    }
}
