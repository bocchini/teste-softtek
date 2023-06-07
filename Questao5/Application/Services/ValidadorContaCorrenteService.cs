using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Repositorio.Interfaces;

namespace Questao5.Application.Services
{
    public class ValidadorContaCorrenteService : IValidadorContaCorrenteService
    {
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;  

        public ValidadorContaCorrenteService(IContaCorrenteRepositorio contaCorrenteRepositorio)
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }

        public async Task<string> Validar(string idContacorrente)
        {
            var contaCorrente = await _contaCorrenteRepositorio.VerificaSeExiteConta(idContacorrente);

            if (contaCorrente == null) return MensagensErroContasCorrente.ContaInvalida;
            if (contaCorrente.Ativo < 1) return MensagensErroContasCorrente.ContaInativa;
            return "";
        }
    }
}
