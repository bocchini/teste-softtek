using Bogus;
using FluentAssertions;
using Questao5.Domain.Entities;
using Questao5.Domain.Validation;

namespace Questoes.Testes.Questao5.Domain.Entities
{
    public class ContaCorrenteTests
    {
        private readonly Faker _faker = new Faker();

        [Fact]
        public void CriaUmaContaCorrente_ComIdDaConta_ComParametrosValidos()
        {
            Action action = () => new ContaCorrente(_faker.Random.String());
            action.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void CriaUmaContaCorrente_ComIdDaContaVazio_DeveRetornarErro()
        {
            Action action = () => new ContaCorrente("");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("O Id da conta Corrente não pode ser nula.");
        }


        [Fact]
        public void CriaUmaContaCorrente_ComTodosParametros_ComParametrosValidos()
        {
            Action action = () => new ContaCorrente(_faker.Random.String(), _faker.Random.Int(0, 100), _faker.Person.FullName, _faker.Random.Int(0, 1));
            action.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void CriaUmaContaCorrente_ComTodosParametrosOIdContaCorrenteInvalido_CriaUmaContaCorrente_ComIdDaContaVazio_DeveRetornarErro()
        {
            Action action = () => new ContaCorrente("", _faker.Random.Int(0, 100), _faker.Person.FullName, _faker.Random.Int(0, 1));
            action.Should().Throw<DomainExceptionValidation>().WithMessage("O Id da conta Corrente não pode ser nula.");
        }

        [Fact]
        public void CriaUmaContaCorrente_ComTodosParametrosONomeCorrentistaInvalido_CriaUmaContaCorrente_ComIdDaContaVazio_DeveRetornarErro()
        {
            Action action = () => new ContaCorrente(_faker.Random.String(), _faker.Random.Int(0, 100), "", _faker.Random.Int(0, 1));
            action.Should().Throw<DomainExceptionValidation>().WithMessage("O Nome do Correntista não pode ser nulo.");
        }
    }
}
