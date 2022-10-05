using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities;

public class Transacao : Entity
{
    public Transacao(DateTime dataTransacao, string descricao, double valor, int numParcela, int totalParcelas, User user, Tipo tipo, FormaPagamento formaPagamento)
    {
        DataTransacao = dataTransacao;
        Descricao = descricao;
        Valor = valor;
        User = user;
        Tipo = tipo;
        FormaPagamento = formaPagamento;
        NumParcela = numParcela;
        TotalParcelas = totalParcelas;
    }
    public Guid UserId { get; set; }
    public Guid TipoId { get; set; } 
    public Guid FormaPagamentoId { get; set; }
    public DateTime DataTransacao { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public int NumParcela { get; set; }
    public int TotalParcelas { get; set; }

    public FormaPagamento FormaPagamento { get; set; }

    public User User { get; set; }
    public Tipo Tipo { get; set; } 

    protected Transacao() { }
}