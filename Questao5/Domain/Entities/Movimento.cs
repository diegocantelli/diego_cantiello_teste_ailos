namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string IdMovimento { get; set; } = default!;
        public string IdContaCorrente { get; set; } = default!;
        public string DataMovimento { get; set; } = default!;
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }

        public ContaCorrente? ContaCorrente { get; set; }
    }
}
