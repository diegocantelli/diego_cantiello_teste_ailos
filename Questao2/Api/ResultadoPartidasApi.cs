using Questao2.Api.Dtos;
using System.Net.Http.Json;

namespace Questao2.Api
{
    public class ResultadoPartidasApi : IResultadoPartidasApi
    {
        private readonly HttpClient _httpClient;

        public ResultadoPartidasApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetResultadosPorTime(string teamName, int year)
        {
            var resultadosTime = new List<int>();
            ResultadosTime? responseTeam1 = new ResultadosTime();
            ResultadosTime? responseTeam2 = new ResultadosTime();

            var golsComoTeam1 = await ObterDadosPaginados(
                page => _httpClient.GetFromJsonAsync<ResultadosTime>($"football_matches?year={year}&team1={teamName}&page={page}"),
                partida => Convert.ToInt32(partida.Team1Goals));

            var golsComoTeam2 = await ObterDadosPaginados(
                page => _httpClient.GetFromJsonAsync<ResultadosTime>($"football_matches?year={year}&team2={teamName}&page={page}"),
                partida => Convert.ToInt32(partida.Team2Goals));

            var totalGols = golsComoTeam1.Concat(golsComoTeam2);
            return ResultadosTime.CalcularGols(totalGols);
        }

        private async Task<List<int>> ObterDadosPaginados(
             Func<int, Task<ResultadosTime?>> resultadoPagina,
             Func<ResultadoPartidaTime, int> seletor)
        {
            var results = new List<int>();
            int page = 1;
            ResultadosTime? response;

            do
            {
                response = await resultadoPagina(page);
                if (response?.Data != null)
                {
                    results.AddRange(response.Data.Select(seletor));
                }
                page++;
            } while (response != null && page <= response.TotalPages);

            return results;
        }

    }
}
