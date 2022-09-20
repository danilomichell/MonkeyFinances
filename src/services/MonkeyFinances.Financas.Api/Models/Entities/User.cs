using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public User() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
