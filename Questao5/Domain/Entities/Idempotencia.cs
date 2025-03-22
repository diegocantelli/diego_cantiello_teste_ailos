namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        public string ChaveIdempotencia { get; set; } = default!;
        public string Requisicao { get; set; } = default!;
        public string Resultado { get; set; } = default!;
    }
}
