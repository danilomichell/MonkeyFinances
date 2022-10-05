using MonkeyFinances.Financas.Api.Models.Enuns;

namespace MonkeyFinances.Financas.Api.Models
{
    public class AddTransaction
    {
        public string Email { get; set; }
        public string Descricao { get; set; }
        public DateTime DataTransacao { get; set; }
        public double Valor { get; set; }
        public EnumTipo Tipo { get; set; }
        public int NumParcela { get; set; }
        public int TotalParcelas { get; set; }
        public EnumFormaPagamento FormaPagamento { get; set; }
    }
}
