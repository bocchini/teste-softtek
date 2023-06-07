using Questao5.Domain.Enumerators;
using Questao5.Domain.Validation;

namespace Questao5.Domain.Entities
{
    public sealed class Movimento: Base
    {
        public string IdMovimento { get; private set; }

        public string DataMovimento { get; private set; }
        public string TipoMovimento { get; private set; }        
        public double Valor { get; private set; }
        public ContaCorrente ContaCorrente { get; set; }

        public Movimento(string idContaCorrente) : base(idContaCorrente) { }

        public Movimento(string idContaCorrente, string idMovimento) : base(idContaCorrente)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(idMovimento),"O Id da da movimentação não pode ser nula.");
            IdMovimento = idMovimento;
        }

        public Movimento(string idContaCorrente, string tipoMovimento, double valor) : base(idContaCorrente)
        {
            IdMovimento = Guid.NewGuid().ToString();
            DataMovimento = DateTime.Now.ToString();
            ValidaTipoMovimentacao(tipoMovimento);
            ValidaValor(valor);
        }
        public Movimento(string idMovimento, string idContaCorrente, string tipoMovimento, string dataMovimento, double valor) : this(idContaCorrente, idMovimento)
        {            
            DataMovimento = dataMovimento;
            ValidaTipoMovimentacao(tipoMovimento);
            ValidaValor(valor);
        }

        public Movimento(string idMovimento, string idContaCorrente, string tipoMovimento, string dataMovimento, double valor, ContaCorrente contaCorrente) : this(idContaCorrente, idMovimento)
        {
            DataMovimento = dataMovimento;
            ValidaTipoMovimentacao(tipoMovimento);
            ValidaValor(valor);
            ContaCorrente = contaCorrente;
        }

        private void ValidaTipoMovimentacao(string tipoMovimentacao)
        {
            if(!(tipoMovimentacao == ETipoMovimento.Debito || tipoMovimentacao == ETipoMovimento.Credito)) 
            throw new ArgumentException(MensagensErroMovimentacao.TipoMovimentacaoCreditoDebito);

            TipoMovimento = tipoMovimentacao;
        }
        private void ValidaValor(double valor)
        {
            DomainExceptionValidation.When(valor < 0, MensagensErroMovimentacao.ValoresNegativos);
            Valor = valor;
        }
    }
}
