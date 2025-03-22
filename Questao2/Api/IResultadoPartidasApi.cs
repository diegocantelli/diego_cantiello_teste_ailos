namespace Questao2.Api
{
    public interface IResultadoPartidasApi
    {
        Task<int> GetResultadosPorTime(string teamName, int year);
    }
}
