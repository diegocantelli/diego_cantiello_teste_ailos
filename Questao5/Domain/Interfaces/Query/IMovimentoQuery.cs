using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Query
{
    public interface IMovimentoQuery
    {
        Task<IEnumerable<Movimento>> GetAllByContaIdAsync(string idContaCorrente);
        Task<Movimento?> GetByIdAsync(string idMovimento);
    }
}
