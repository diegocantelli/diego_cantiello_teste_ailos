using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Language;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/saldo")]
        [SwaggerOperation(
            Summary = "Consulta o saldo da conta corrente",
            Description = "Retorna o saldo atual da conta, considerando todos os movimentos realizados até o momento."
        )]
        [SwaggerResponse(200, "Consulta realizada com sucesso", typeof(ConsultarSaldoResponse))]
        [SwaggerResponse(400, "Conta inválida ou inativa", typeof(ErroResponse))]
        public async Task<IActionResult> ConsultarSaldo(string id)
        {
            try
            {
                var result = await _mediator.Send(new ConsultarSaldoQuery { IdContaCorrente = id });
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new ErroResponse
                {
                    Tipo = ex.Type,
                    Mensagem = ex.Message
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ErroResponse
                {
                    Tipo = "Erro desconhecido",
                    Mensagem = ex.Message
                });
            }
        }
    }
}
