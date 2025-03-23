using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Application.Queries.Responses
{
    [SwaggerSchema("Modelo de retorno para uma consulta de saldo efetuada com sucesso.")]
    public class ConsultarSaldoResponse
    {
        [SwaggerSchema("Número da conta.")]
        public int Numero { get; set; }


        [SwaggerSchema("Nome do titular da conta.")]
        public string Nome { get; set; } = default!;


        [SwaggerSchema("Data em que a consulta foi efetuada.")]
        public DateTime DataResposta { get; set; }


        [SwaggerSchema("Valor que consta na conta bancária.")]
        public decimal Saldo { get; set; }
    }
}
