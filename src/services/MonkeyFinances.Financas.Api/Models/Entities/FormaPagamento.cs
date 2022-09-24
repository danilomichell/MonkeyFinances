using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities;

public class FormaPagamento : Entity
{
    public string Descricao { get; set; } = null!;
    public ICollection<Parcela> Parcelas { get; set; } = null!;
}