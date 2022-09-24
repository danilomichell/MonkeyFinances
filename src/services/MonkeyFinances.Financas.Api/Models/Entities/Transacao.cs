using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities;

public class Transacao : Entity
{
    public Guid IdUser { get; set; }
    public Guid IdTipo { get; set; }
    public Guid IdParcela { get; set; }
    public DateTime DataTransacao { get; set; }
    public string Descricao { get; set; } = null!;
    public double Valor { get; set; }

    public User User { get; set; } = null!;
    public Tipo Tipo { get; set; } = null!;
    public Parcela Parcela { get; set; } = null!;
}