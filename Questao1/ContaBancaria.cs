using System;
using System.Globalization;

namespace Questao1
{
    public sealed class ContaBancaria 
    {
        private const double TAXA_Bancaria = 3.50;
        public int NumeroDaConta { get; private set; }
        public string Titular { get; private set; }

        public double Saldo { get; private set; }

        public ContaBancaria(int numeroDaConta, string titular)
         {
            NumeroDaConta = numeroDaConta;
            Titular = titular;
            Saldo = 0;
        }

        public ContaBancaria(int numeroDaConta, string titular, double depositoInicial) : this(numeroDaConta, titular)
        {            
            Saldo = depositoInicial;
        }

        public void Deposito(double deposito) =>
            Saldo += deposito;
        
        public void Saque(double saque) =>
           Saldo -= ( saque + TAXA_Bancaria);
        

        public void AlteraNomeTitular(string titular)
        {
            Titular = titular;
        }

        public override string ToString()
        {
            return $"Conta {NumeroDaConta}, Titular: {Titular}, Saldo: $ {String.Format("{0:#,##0.00}",Saldo)}";
        }
    }
}
