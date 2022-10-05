using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public User() { }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();


        public void AddTransaction(string descricao, DateTime dataTransacao, double valor, Tipo tipo,
            int numParcela, int totalParcela, FormaPagamento formaPagamento)
        {
            Transacoes.Add(new Transacao
            {
                Descricao = descricao,
                DataTransacao = dataTransacao,
                Valor = valor,
                Tipo = tipo,
                //Parcela = new Parcela
                //{
                //    NumParcela = numParcela,
                //    TotalParcelas = totalParcela,
                //    FormaPagamento = formaPagamento
                //}
            });
        }
    }
}
