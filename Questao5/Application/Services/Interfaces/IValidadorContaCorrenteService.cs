namespace Questao5.Application.Services.Interfaces
{
    public interface IValidadorContaCorrenteService
    {
        Task<string> Validar(string idContacorrente);
    }
}
