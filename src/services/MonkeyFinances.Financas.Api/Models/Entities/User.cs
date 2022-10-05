using MonkeyFinances.Core.DomainObject;

namespace MonkeyFinances.Financas.Api.Models.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public User(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        public string Name { get; set; }  
        public string Email { get; set; }  
        public ICollection<Transacao> Transacoes { get; set; } 
        protected User() { }

        //public void AddTransaction(string descricao, DateTime dataTransacao, double valor, Tipo tipo,
        //    int numParcela, int totalParcela, FormaPagamento formaPagamento)
        //{
        //    Transacoes.Add(new Transacao
        //    {
        //        Descricao = descricao,
        //        DataTransacao = dataTransacao,
        //        Valor = valor,
        //        Tipo = tipo,
        //        Parcela = new Parcela
        //        {
        //            NumParcela = numParcela,
        //            TotalParcelas = totalParcela,
        //            FormaPagamento = formaPagamento
        //        }
        //    });
        //}
    }
}
