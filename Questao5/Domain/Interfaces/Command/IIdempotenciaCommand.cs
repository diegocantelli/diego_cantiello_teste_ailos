using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Command
{
    public interface IIdempotenciaCommand
    {
        Task InserirAsync(Idempotencia idem);
    }
}
