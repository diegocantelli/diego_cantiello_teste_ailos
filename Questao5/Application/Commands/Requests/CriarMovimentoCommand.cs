using MediatR;

namespace Questao5.Application.Commands.Requests
{
    public class CriarMovimentoCommand : IRequest<string>
    {
        public string IdRequisicao { get; set; } = default!;
        public string IdContaCorrente { get; set; } = default!;
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; } = default!;
    }
}
