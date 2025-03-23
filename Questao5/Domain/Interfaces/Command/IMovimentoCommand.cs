using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Command
{
    public interface IMovimentoCommand
    {
        Task InserirAsync(Movimento movimento);
    }
}
