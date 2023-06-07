using AutoMapper;
using Questao5.Application.Dtos;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Repositorio;
using Questao5.Infrastructure.Repositorio.Interfaces;

namespace Questao5.Application.Services
{
    public class SaldoService : ISaldoService
    {
        private readonly ISaldoContaCorrenteRepositorio _repositorio;
        private double SaldoContaCorrente = 0;
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;
        private readonly IMapper _mapper;

        public SaldoService(ISaldoContaCorrenteRepositorio repositorio, IMapper mapper, IContaCorrenteRepositorio contaCorrenteRepositorio)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }

        public async Task<SaldoDto> BuscaSaldo(string idContaCorrente)
        {
            var movimentacao = await _repositorio.SaldoAsync(idContaCorrente);
            var saldo = new Saldo();
            if (movimentacao.Count > 0)
            {
                foreach (var item in movimentacao)
                {
                    CalcularSaldo(item.Valor, item.TipoMovimento);
                }

                var dadosContaCorrente = movimentacao.FirstOrDefault();
                saldo = new Saldo(dadosContaCorrente.Numero, dadosContaCorrente.Nome);
                saldo.SaldoConta = SaldoContaCorrente;
            }
            else
            {
                var contaCorrente = await _contaCorrenteRepositorio.VerificaSeExiteConta(idContaCorrente);
                saldo = new Saldo(contaCorrente.Numero, contaCorrente.Nome);
                saldo.SaldoConta = 0;
            }

            return _mapper.Map<SaldoDto>(saldo);
        }     

        private void CalcularSaldo(double valor, string tipoMovimento)
        {
            if (ETipoMovimento.Credito.Equals(tipoMovimento)) SaldoContaCorrente += valor;
            else if (ETipoMovimento.Debito.Equals(tipoMovimento)) SaldoContaCorrente -= valor;
        }
    }
}
