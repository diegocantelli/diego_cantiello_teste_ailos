using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Query
{
    public interface IContaCorrenteQuery
    {
        Task<ContaCorrente?> GetByIdAsync(string id);
        Task<ContaCorrente?> GetByNumeroAsync(int numero);
    }
}
