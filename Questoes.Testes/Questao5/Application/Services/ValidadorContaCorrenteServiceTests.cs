using Bogus;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Services;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Repositorio.Interfaces;

namespace Questoes.Testes.Questao5.Application.Services
{
    public class ValidadorContaCorrenteServiceTests
    {
        private readonly ValidadorContaCorrenteService _service;
        private readonly IContaCorrenteRepositorio _repositorio;
        private Faker _faker = new Faker();

        public ValidadorContaCorrenteServiceTests()
        {
            _repositorio = Substitute.For<IContaCorrenteRepositorio>();
            _service = new ValidadorContaCorrenteService(_repositorio);
        }

        [Fact]
        public async void Deve_RetornarContaInvalida_QuandoNaoExistirContaCorrente()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            _repositorio.VerificaSeExiteConta(Arg.Any<string>()).ReturnsNull();
            var expectResult = MensagensErroContasCorrente.ContaInvalida;
            var response = await _service.Validar(idContaCorrente);

            response.Should().Be(expectResult);
        }

        [Fact]
        public async void Deve_RetornarContaInativa_QuandoContaCorrenteEstiverInativa()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            var conta = new ContaCorrente(idContaCorrente,_faker.Random.Int(0,100),_faker.Person.FullName,0);
            _repositorio.VerificaSeExiteConta(Arg.Any<string>()).Returns(conta);
            var expectResult = MensagensErroContasCorrente.ContaInativa;
            var response = await _service.Validar(idContaCorrente);

            response.Should().Be(expectResult);
        }

        [Fact]
        public async void Deve_RetornarStringVazia_QuandoContaCorrenteEstiverCorreta()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            var conta = new ContaCorrente(idContaCorrente, _faker.Random.Int(0, 100), _faker.Person.FullName, 1);
            _repositorio.VerificaSeExiteConta(Arg.Any<string>()).Returns(conta);
            
            var response = await _service.Validar(idContaCorrente);

            response.Should().Be("");
        }
    }
}
