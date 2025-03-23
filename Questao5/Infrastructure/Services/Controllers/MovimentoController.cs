using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Language;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um movimento para uma determinada conta bancária", Description = "Registra um débito ou crédito para uma determinada conta bancária.")]
        [SwaggerResponse(200, "Movimento registrado com sucesso", typeof(string))]
        [SwaggerResponse(400, "Erro ao registrar movimento", typeof(ErroResponse))]
        public async Task<IActionResult> Post([FromBody] CriarMovimentoCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
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
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse
                {
                    Tipo = "Erro desconhecido",
                    Mensagem = ex.Message
                });
            }
        }
    }

    [SwaggerSchema("Modelo de resposta para caso de erro na requisição")]
    public class ErroResponse
    {
        [SwaggerSchema("Tipo do erro")]
        public string Tipo { get; set; } = default!;

        [SwaggerSchema("Descrição da mensagem de erro")]
        public string Mensagem { get; set; } = default!;
    }
}
