using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Language;

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
        [ProducesResponseType(typeof(ConsultarSaldoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
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
