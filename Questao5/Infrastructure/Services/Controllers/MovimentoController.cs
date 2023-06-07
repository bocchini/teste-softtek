using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questao5.Application.Dtos;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentoController:ControllerBase
    {
        private readonly IMovimentoService _service;

        public MovimentoController(IMovimentoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Movimentacao(MovimentoDto movimento)
        {
            var idMovimento = await _service.AdicionaMovimento(movimento);
            if (idMovimento == null || idMovimento == MensagensErroContasCorrente.ContaInativa || idMovimento == MensagensErroContasCorrente.ContaInvalida)
            {
                return BadRequest(JsonConvert.SerializeObject(new {Error= idMovimento}));
            }
            var json = JsonConvert.SerializeObject(new { IdMovimento = idMovimento});
            return Ok(json);
        }
    }
}
