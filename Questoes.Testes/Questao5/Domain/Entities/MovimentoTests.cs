using Bogus;
using FluentAssertions;
using Questao5.Domain.Entities;
using Questao5.Domain.Validation;

namespace Questoes.Testes.Questao5.Domain.Entities
{
    public class MovimentoTests
    {
        private readonly Faker _faker = new Faker();

        [Fact]
        public void CriaUmaMovimentacao_ComIdDaConta_ComParametrosValidos()
        {
            Action action = () => new Movimento(_faker.Random.String());
            action.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void CriaUmaMovimentacao_ComIdDaContaInvalido_DeveRetornarErro()
        {
            Action action = () => new Movimento("");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("O Id da conta Corrente não pode ser nula.");
        }

        [Fact]
        public void CriaUmaMovimentacao_ComTodosParametros_ComParametrosValidos()
        {
            Action action = () => new Movimento(_faker.Random.String(), _faker.Random.String());
            action.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void CriaUmaMovimentacao_ComIdMovimentoInvalido_DeveRetornarErro()
        {
            Action action = () => new Movimento(_faker.Random.String(), "");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("O Id da da movimentação não pode ser nula.");
        }
    }
}
