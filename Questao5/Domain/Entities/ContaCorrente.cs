namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; } = default!;
        public int Numero { get; set; }
        public string Nome { get; set; } = default!;
        public bool Ativo { get; set; }

        public ICollection<Movimento>? Movimentos { get; set; }
    }
}
