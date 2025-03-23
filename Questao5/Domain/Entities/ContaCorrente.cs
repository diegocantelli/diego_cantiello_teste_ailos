using Questao5.Domain.Constants;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; } = default!;
        public int Numero { get; set; }
        public string Nome { get; set; } = default!;
        public bool Ativo { get; set; }

        public IEnumerable<Movimento>? Movimentos { get; set; }

        public decimal Saldo()
        {
            if (Movimentos?.Any() == false)
            {
                return 0;
            }

            var totalCreditos = Movimentos?.Where(m => m.TipoMovimento == TipoMovimento.Credito)?.Sum(m => m.Valor) ?? 0;
            var totalDebitos = Movimentos?.Where(m => m.TipoMovimento == TipoMovimento.Debito).Sum(m => m.Valor) ?? 0;
            return totalCreditos - totalDebitos;
        }
    }
}
