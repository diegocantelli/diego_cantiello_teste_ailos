using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Language;

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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
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
        }
    }

    public class ErroResponse
    {
        public string Tipo { get; set; } = default!;
        public string Mensagem { get; set; } = default!;
    }
}
