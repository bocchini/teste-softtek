using Bogus;
using FluentAssertions;
using Questao1;
using System.Xml.Serialization;

namespace Questoes.Testes.Questao1;

public class ContaBancariaTestes
{
    private readonly ContaBancaria _contaBancaria;
    private Faker _faker;

    public ContaBancariaTestes()
    {
        _faker = new Faker();
        _contaBancaria = new ContaBancaria(_faker.Random.Int(1000,9999),_faker.Person.FullName, Convert.ToDouble(_faker.Random.Decimal()));
    }
    [Fact]
    public void Deve_CriarUmaContaBancaria_EMostrarDadossCorretamente()
    {
        var conta = _contaBancaria.ToString();
        var esperado = $"Conta {_contaBancaria.NumeroDaConta}, Titular: {_contaBancaria.Titular}, Saldo: $ {String.Format("{0:#,##0.00}", _contaBancaria.Saldo)}";
        esperado.Should().BeEquivalentTo(conta);
    }

    [Fact]
    public void Deve_CriarUmaContaBancariaSemSaldoInicial_EMostrarDadossCorretamente()
    {
        var conta = new ContaBancaria(_faker.Random.Int(1000, 9999), _faker.Person.FullName);
        var esperado = $"Conta {conta.NumeroDaConta}, Titular: {conta.Titular}, Saldo: $ 0,00";
        esperado.Should().BeEquivalentTo(conta.ToString());
    }

    [Fact]
    public void Deve_Depositar_MostrarOSaldoAtualizado()
    {
        var deposito = Convert.ToDouble(_faker.Random.Decimal());
        var saldoEsperado = deposito + _contaBancaria.Saldo;

        _contaBancaria.Deposito(deposito);

        saldoEsperado.Should().Be(_contaBancaria.Saldo);
    }

    [Fact]
    public void Deve_Sacar_MostrarOSaldoSacadoComTaxa()
    {
        var taxa = 3.50;
        var saque = Convert.ToDouble(_faker.Random.Decimal());
        var saldoEsperado = _contaBancaria.Saldo - ( taxa + saque);

        _contaBancaria.Saque(saque);

        saldoEsperado.Should().Be(_contaBancaria.Saldo);
    }

    [Fact]
    public void Deve_Alterar_NomeDoTitualr()
    {
        var titular = _faker.Person.FullName;
        _contaBancaria.AlteraNomeTitular(titular);

        titular.Should().Be(_contaBancaria.Titular);
    }

   
   
}
