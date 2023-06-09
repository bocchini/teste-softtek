using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Enumerators;
using Questao5.Application.Services.Interfaces;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaldoController : ControllerBase
    {
        private readonly ISaldoService _service;
        private readonly IValidadorContaCorrenteService _validadorContaCorrenteService;


        public SaldoController(ISaldoService service, IValidadorContaCorrenteService validadorContaCorrenteService)
        {
            _service = service;
            _validadorContaCorrenteService = validadorContaCorrenteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string idContaCorrente)
        {
            var validar = await _validadorContaCorrenteService.Validar(idContaCorrente);
            if (validar == MensagensErroContasCorrente.ContaInativa || validar == MensagensErroContasCorrente.ContaInvalida)
            {
                return BadRequest(JsonConvert.SerializeObject(new { Error = validar }));
            }
            return Ok(await _service.BuscaSaldo(idContaCorrente));
        }
    }
}
