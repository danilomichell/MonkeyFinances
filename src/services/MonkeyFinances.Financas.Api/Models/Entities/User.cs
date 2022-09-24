using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public User() { }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Transacao> Transacoes { get; set; } = null!;

    }
}
