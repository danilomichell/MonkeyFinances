using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities;

public class Tipo : Entity
{
    public string Descricao { get; set; } = null!;
    public ICollection<Transacao> Transacoes { get; set; } = null!;
}