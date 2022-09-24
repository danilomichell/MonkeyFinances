using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities;

public class Parcela : Entity
{
    public Guid IdTransacao { get; set; }
    public Guid IdFormaPagamento { get; set; }
    public int NumParcela { get; set; }
    public int TotalParcelas { get; set; }

    public FormaPagamento FormaPagamento { get; set; } = null!;
    public Transacao Transacao { get; set; } = null!;
}