using AutoMapper;
using Questao5.Domain.Entities;
using Questao5.Application.Dtos;
using Questao5.Application.Services.Interfaces;
using Questao5.Infrastructure.Repositorio.Interfaces;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Services
{
    public class MovimentoService: IMovimentoService
    {
        private readonly IMovimentoRepositorio _repositorio;
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;

        private readonly IMapper _mapper;

        public MovimentoService(IMovimentoRepositorio repositorio, IContaCorrenteRepositorio contaCorrenteRepositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
            _mapper = mapper;
        }

        public async Task<string> AdicionaMovimento(MovimentoDto movimentoDto)
        {
            var movimento = _mapper.Map<Movimento>(movimentoDto);
             var contaCorrente = await _contaCorrenteRepositorio.VerificaSeExiteConta(movimento.IdContaCorrente);

            if (contaCorrente == null) return MensagensErroContasCorrente.ContaInvalida;
            if (contaCorrente.Ativo < 1) return MensagensErroContasCorrente.ContaInativa;
            
            return await _repositorio.CriaMovimentoAsync(movimento);            
        }
    }
}
