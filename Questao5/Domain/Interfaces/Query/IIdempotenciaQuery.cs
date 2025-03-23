using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Query
{
    public interface IIdempotenciaQuery
    {
        Task<Idempotencia?> GetByChaveAsync(string chave);
    }
}
