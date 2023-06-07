using NSubstitute;
using Questao5.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public sealed class ContaCorrente: Base
    {
        [Column("numero")]
        public Int64 Numero { get; private set; }

        [Column("nome")]
        public string Nome { get; private set; }

        [Column("ativo")]
        public Int64 Ativo { get; private set; }

        public ICollection<Movimento> Movimentacao { get; set; }

        public ContaCorrente(string idContaCorrente) : base(idContaCorrente) { }

        public ContaCorrente(string idContaCorrente, Int64 numero, string nome, Int64 ativo):base(idContaCorrente)
        {
            
            ValidacaoContaCorrente(numero, nome, ativo);
        }

        private void ValidacaoContaCorrente(Int64 numero, string nome, Int64 ativo)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "O Nome do Correntista não pode ser nulo.");
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
        }
    }
}
