using Bogus;
using NSubstitute;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Dtos;
using FluentAssertions.Execution;
using Questao5.Domain.Enumerators;
using Questao5.Application.Services.Interfaces;
using Questao5.Infrastructure.Services.Controllers;

namespace Questoes.Testes.Questao5.Infrastructure.Services.Controllers
{
    public class SaldoControllerTests
    {
        private readonly SaldoController _controller;
        private readonly ISaldoService _saldoService;
        private readonly IValidadorContaCorrenteService _validadorContaCorrenteService;
        private Faker _faker = new Faker();

        public SaldoControllerTests()
        {
            _saldoService = Substitute.For< ISaldoService>();
            _validadorContaCorrenteService = Substitute.For<IValidadorContaCorrenteService>();
            _controller = new SaldoController(_saldoService, _validadorContaCorrenteService);
        }

        [Fact]
        public async void Deve_RetornarBadRequestEINACTIVE_ACCOUNT_QuandoAContaFOrInativa()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            _validadorContaCorrenteService.Validar(Arg.Any<string>()).Returns(MensagensErroContasCorrente.ContaInativa);
           
            var result = (BadRequestObjectResult)await _controller.Get(idContaCorrente);

            var messageExpect = JsonConvert.SerializeObject(new { Error = MensagensErroContasCorrente.ContaInativa });
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(400);
                result.Value.Should().Be(messageExpect);
                result.ContentTypes.Should().BeEmpty();
            }
        }


        [Fact]
        public async void Deve_RetornarBadRequestEINVALID_ACCOUNT_QuandoAContaForIvalida()
        {
            var idContaCorrente = _faker.Random.Guid().ToString();
            _validadorContaCorrenteService.Validar(Arg.Any<string>()).Returns(MensagensErroContasCorrente.ContaInvalida);

            var result = (BadRequestObjectResult)await _controller.Get(idContaCorrente);

            var messageExpect = JsonConvert.SerializeObject(new { Error = MensagensErroContasCorrente.ContaInvalida });
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(400);
                result.Value.Should().Be(messageExpect);
                result.ContentTypes.Should().BeEmpty();
            }
        }

        [Fact]
        public async void Deve_RetornarStatuOkESaldo_QuandoAContaTiverMovimentacaoAcimaDeZero()
        {
            string idContaCorrente = _faker.Random.Guid().ToString();
            var saldoDto = new SaldoDto()
            {
                Nome = _faker.Random.Guid().ToString(),
                Numero = _faker.Random.Int(1, 100),
                SaldoConta = _faker.Random.Double(0, 1),
                HoraConsulta = DateTime.Now.ToString()
            };

            _validadorContaCorrenteService.Validar(Arg.Any<string>()).Returns("");
            _saldoService.BuscaSaldo(idContaCorrente).Returns(saldoDto);

            var result = (OkObjectResult)await _controller.Get(idContaCorrente);

            var saldoEsperado = new SaldoDto()
            {
                Nome = saldoDto.Nome,
                Numero = saldoDto.Numero,
                SaldoConta = saldoDto.SaldoConta,
                HoraConsulta = saldoDto.HoraConsulta
            };
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(200);
                result.Value.Should().BeEquivalentTo(saldoEsperado);
            }
        }

        [Fact]
        public async void Deve_RetornarStatuOkERetornarTodosDados_QuandoTiverMovimentaZero()
        {
            string idContaCorrente = _faker.Random.Guid().ToString();
            var saldo = new SaldoDto()
            {
                Nome = _faker.Random.Guid().ToString(),
                Numero = _faker.Random.Int(1, 100),
                SaldoConta = 0,
                HoraConsulta = DateTime.Now.ToString()
            };
            _validadorContaCorrenteService.Validar(Arg.Any<string>()).Returns("");

            _saldoService.BuscaSaldo(idContaCorrente).Returns(saldo);

            var result = (OkObjectResult)await _controller.Get(idContaCorrente);

            var saldoEsperado = new SaldoDto()
            {
                Nome = saldo.Nome,
                Numero = saldo.Numero,
                SaldoConta = saldo.SaldoConta,
                HoraConsulta = saldo.HoraConsulta
            };
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(200);
                result.Value.Should().BeEquivalentTo(saldoEsperado);
            }
        }
    }
}
