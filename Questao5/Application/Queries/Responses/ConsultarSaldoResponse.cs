namespace Questao5.Application.Queries.Responses
{
    public class ConsultarSaldoResponse
    {
        public int Numero { get; set; }

        public string Nome { get; set; } = default!;

        public DateTime DataResposta { get; set; }

        public decimal Saldo { get; set; }
    }
}
