using Bogus;
using AutoMapper;
using NSubstitute;
using FluentAssertions;
using Questao5.Domain.Entities;
using Questao5.Application.Dtos;
using Questao5.Domain.Enumerators;
using Questao5.Application.Services;
using NSubstitute.ReturnsExtensions;
using Questao5.Infrastructure.Repositorio.Interfaces;

namespace Questoes.Testes.Questao5.Application.Services
{
    public class MovimentoServiceTests
    {
        private readonly IMovimentoRepositorio _movimentoRepositorio;
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;
        private readonly IMapper _mapper;
        private readonly MovimentoService _movimentoService;
        private Faker _faker = new Faker();

        public MovimentoServiceTests()
        {
            _movimentoRepositorio =  Substitute.For< IMovimentoRepositorio>();
            _contaCorrenteRepositorio = Substitute.For< IContaCorrenteRepositorio>();
            _mapper = Substitute.For<IMapper>();
            _movimentoService = new MovimentoService(_movimentoRepositorio, _contaCorrenteRepositorio, _mapper);
        }
       
        [Fact]
        public async void Deve_RetornarContaInvalida_QuandoContaCorrenteEstiverInvalida()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            var movimentoDto = new MovimentoDto()
            {
                IdContaCorrente = idContaCorrente,
            };

            var movimento = new Movimento(idContaCorrente);
            _mapper.Map<Movimento>(movimentoDto).Returns(movimento);
            _contaCorrenteRepositorio.VerificaSeExiteConta(Arg.Any<string>()).ReturnsNull();
            var expectResult = MensagensErroContasCorrente.ContaInvalida;
            var response = await _movimentoService.AdicionaMovimento(movimentoDto);

            response.Should().Be(expectResult);
        }

        [Fact]
        public async void Deve_RetornarContaInativa_QuandoContaCorrenteEstiverInativa()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            var movimentoDto = new MovimentoDto()
            {
                IdContaCorrente = idContaCorrente,
            };

            var conta = new ContaCorrente(idContaCorrente, _faker.Random.Int(0, 100), _faker.Person.FullName, 0);

            var movimento = new Movimento(idContaCorrente);
            _mapper.Map<Movimento>(movimentoDto).Returns(movimento);
            _contaCorrenteRepositorio.VerificaSeExiteConta(Arg.Any<string>()).Returns(conta);
            var expectResult = MensagensErroContasCorrente.ContaInativa;
            var response = await _movimentoService.AdicionaMovimento(movimentoDto);

            response.Should().Be(expectResult);
        }

        [Fact]
        public async void Deve_RetornarIdMovimento_QuandoContaCorrenteEstiverAtendendoTodosRequisitos()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            var movimentoDto = new MovimentoDto()
            {
                IdContaCorrente = idContaCorrente,
            };

            var conta = new ContaCorrente(idContaCorrente, _faker.Random.Int(0, 100), _faker.Person.FullName, 1);

            var movimento = new Movimento(idContaCorrente);
            _mapper.Map<Movimento>(movimentoDto).Returns(movimento);
            _contaCorrenteRepositorio.VerificaSeExiteConta(Arg.Any<string>()).Returns(conta);
                _movimentoRepositorio.CriaMovimentoAsync(Arg.Any<Movimento>()).Returns(idContaCorrente);
            var expectResult = idContaCorrente;
            var response = await _movimentoService.AdicionaMovimento(movimentoDto);

            response.Should().Be(expectResult);
        }
    }
}
