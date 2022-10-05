using FluentValidation;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Models.Enuns;

namespace MonkeyFinances.Financas.Api.Application.Commands.AddTransaction;

public class AddTransactionCommand : Command
{
    public string Email { get; set; }
    public string Descricao { get; set; }
    public DateTime DataTransacao { get; set; }
    public double Valor { get; set; }
    public EnumTipo Tipo { get; set; }
    public int NumParcela { get; set; }
    public int TotalParcelas { get; set; }
    public EnumFormaPagamento FormaPagamento { get; set; }

    public AddTransactionCommand(string email, string descricao, DateTime dataTransacao, double valor, EnumTipo tipo, int numParcela, int totalParcelas, EnumFormaPagamento formaPagamento)
    {
        Email = email;
        Descricao = descricao;
        DataTransacao = dataTransacao;
        Valor = valor;
        Tipo = tipo;
        NumParcela = numParcela;
        TotalParcelas = totalParcelas;
        FormaPagamento = formaPagamento;
    }
    public override bool EhValido()
    {
        ValidationResult = new RegistrarClienteValidation().Validate(this);
        return ValidationResult.IsValid;
    }
    public class RegistrarClienteValidation : AbstractValidator<AddTransactionCommand>
    {
        public RegistrarClienteValidation()
        {
        }
    }
}