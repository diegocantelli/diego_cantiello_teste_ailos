using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Application.Commands.Requests
{
    [SwaggerSchema("Request  para criar um movimento para uma conta bancária")]
    public class CriarMovimentoCommand : IRequest<string>
    {
        [SwaggerSchema("Id único para garantir idempotencia.", Nullable = false)]
        public string IdRequisicao { get; set; } = default!;

        [SwaggerSchema("Identificador da conta corrente.")]
        public string IdContaCorrente { get; set; } = default!;

        [SwaggerSchema("Valor para ser creditado ou debitado. O Valor deve ser positivo.")]
        public decimal Valor { get; set; }

        [SwaggerSchema("Tipo do movimento. 'C' para crédito ou 'D' para débito.")]
        public char TipoMovimento { get; set; } = default!;
    }
}
