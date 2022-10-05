using System.ComponentModel;

namespace MonkeyFinances.Financas.Api.Models.Enuns;

public enum EnumFormaPagamento
{
    [Description("PIX")]
    Pix = 1,
    [Description("Dinheiro")]
    Dinheiro = 2,
    [Description("Credito")]
    Credito = 3,
    [Description("Debito")]
    Debito = 4
}