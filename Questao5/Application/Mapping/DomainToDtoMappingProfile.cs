using AutoMapper;
using Questao5.Domain.Entities;
using Questao5.Application.Dtos;

namespace Questao5.Application.Mapping
{
    public class DomainToDtoMappingProfile:Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Movimento, MovimentoDto>().ReverseMap();
            CreateMap<Saldo, SaldoDto>().ReverseMap();
        }
    }
}
