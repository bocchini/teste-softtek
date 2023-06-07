using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSubstitute;
using Questao5.Application.Dtos;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Enumerators;
using Questoes.Testes.Shared.Extensions;
using Questao5.Infrastructure.Services.Controllers;

namespace Questoes.Testes.Questao5.Infrastructure.Services.Controllers
{
    public class MovimentoControllerTests
    {
        private readonly MovimentoController _controller;
        private readonly IMovimentoService _movimentoService;
        private Faker _faker = new Faker();

        public MovimentoControllerTests()
        {
            _movimentoService = Substitute.For<IMovimentoService>();
            _controller = new MovimentoController(_movimentoService);
        }

        [Fact]
        public async void Deve_RetornarBadRequestEINACTIVE_ACCOUNT_QuandoAContaFOrInativa()
        {
            _movimentoService.AdicionaMovimento(Arg.Any<MovimentoDto>()).Returns(MensagensErroContasCorrente.ContaInativa);
            var movimentacaoDto = new MovimentoDto { IdContaCorrente = _faker.Random.String(), TipoMovimento = "C", Valor = _faker.Random.Double(0, 1) };
            var result =(BadRequestObjectResult) await _controller.Movimentacao(movimentacaoDto);

            var messageExpect = JsonConvert.SerializeObject(new { Error = MensagensErroContasCorrente.ContaInativa });
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(400);
                result.Value.Should().Be(messageExpect);
                result.ContentTypes.Should().BeEmpty();
            }
        }

        [Fact]
        public async void Deve_RetornarBadRequestEINVALID_ACCOUNT_QuandoAContaForIvalidaa()
        {
            _movimentoService.AdicionaMovimento(Arg.Any<MovimentoDto>()).Returns(MensagensErroContasCorrente.ContaInvalida);
            var movimentacaoDto = new MovimentoDto { IdContaCorrente = _faker.Random.String(), TipoMovimento = "C", Valor = _faker.Random.Double(0, 1) };
            var result = (BadRequestObjectResult)await _controller.Movimentacao(movimentacaoDto);

            var messageExpect = JsonConvert.SerializeObject(new { Error = MensagensErroContasCorrente.ContaInvalida });
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(400);
                result.Value.Should().Be(messageExpect);
                result.ContentTypes.Should().BeEmpty();
            }
        }

        [Fact]
        public async void Deve_RetornarStatuOk_QuandoCadastrarUmaMovimentacao()
        {
            string idMovimento = _faker.Random.Guid().ToString();
            _movimentoService.AdicionaMovimento(Arg.Any<MovimentoDto>()).Returns(idMovimento);
            var movimentacaoDto = new MovimentoDto { IdContaCorrente = _faker.Random.String(), TipoMovimento = "C", Valor = _faker.Random.Double(0, 1) };
            var result = (OkObjectResult)await  _controller.Movimentacao(movimentacaoDto);

            var messageExpect = JsonConvert.SerializeObject(new { IdMovimento = idMovimento });

            using (new AssertionScope()) {
                result.StatusCode.Should().Be(200);
                result.Value.Should().BeEquivalentTo(messageExpect);
            }
        }
    }
}
