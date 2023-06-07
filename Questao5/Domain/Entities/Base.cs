using Questao5.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public abstract class Base
    {
        [Column("idcontacorrente")]
        public string IdContaCorrente { get; private set; }

        public Base(string idContaCorrente) =>
          ValidacaoIdContaCorrente(idContaCorrente);
        private void ValidacaoIdContaCorrente(string idContaCorrente)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(idContaCorrente), "O Id da conta Corrente não pode ser nula.");

            IdContaCorrente = idContaCorrente;
        }

    }
}
