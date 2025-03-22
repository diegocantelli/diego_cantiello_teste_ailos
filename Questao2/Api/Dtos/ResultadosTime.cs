using System.Text.Json.Serialization;

namespace Questao2.Api.Dtos
{
    public class ResultadosTime
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; } = int.MaxValue;

        [JsonPropertyName("data")]
        public List<ResultadoPartidaTime> Data { get; set; } = new();


        public static int CalcularGols(IEnumerable<int> partidas)
        {
            if(partidas?.Any() == true)
            {
                return partidas.Sum(x => x);
            }

            return 0;
        }
    }
}
