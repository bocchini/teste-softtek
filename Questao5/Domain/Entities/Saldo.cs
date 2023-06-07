using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Saldo
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public double Valor { get; set; }
        public Int64 Numero { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public string HoraConsulta { get; set; }
        public double SaldoConta { get; set; }

        public Saldo() { }

        public Saldo(Int64 numero, string nome)
        {
            Numero = numero;
            Nome = nome;
            HoraConsulta = DateTime.Now.ToString();
        }

        public Saldo(string idMovimento, string idContaCorrente, string dataMovimento, string tipoMovimento, double valor, long numero, string nome, string ativo)
        {
            IdMovimento = idMovimento;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
        }
    }
}
