using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers.Validators;
using Questao5.Domain.Entities;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Application.Handlers
{
    public class CriarMovimentoCommandHandler : IRequestHandler<CriarMovimentoCommand, string>
    {
        private readonly ContaCorrenteQuery _contaQuery;
        private readonly MovimentoCommand _movimentoCommand;
        private readonly IdempotenciaCommand _idempotenciaCommand;
        private readonly IdempotenciaQuery _idempotenciaQuery;

        public CriarMovimentoCommandHandler(
            ContaCorrenteQuery contaQuery,
            MovimentoCommand movimentoCommand,
            IdempotenciaCommand idempotenciaCommand,
            IdempotenciaQuery idempotenciaQuery)
        {
            _contaQuery = contaQuery;
            _movimentoCommand = movimentoCommand;
            _idempotenciaCommand = idempotenciaCommand;
            _idempotenciaQuery = idempotenciaQuery;
        }

        public async Task<string> Handle(CriarMovimentoCommand request, CancellationToken cancellationToken)
        {
            var idem = await _idempotenciaQuery.GetByChaveAsync(request.IdRequisicao);

            if (idem != null)
                return idem.Resultado;

            var conta = await _contaQuery.GetByIdAsync(request.IdContaCorrente);
            MovimentoValidator.ValidarMovimento(conta, request.Valor, request.TipoMovimento);

            var movimento = new Movimento
            {
                IdMovimento = Guid.NewGuid().ToString().ToUpper(),
                IdContaCorrente = request.IdContaCorrente,
                DataMovimento = DateTime.Now.ToString("dd/MM/yyyy"),
                TipoMovimento = request.TipoMovimento[0],
                Valor = request.Valor
            };

            await _movimentoCommand.InserirAsync(movimento);

            await _idempotenciaCommand.InserirAsync(new Idempotencia
            {
                ChaveIdempotencia = request.IdRequisicao,
                Requisicao = System.Text.Json.JsonSerializer.Serialize(request),
                Resultado = movimento.IdMovimento
            });

            return movimento.IdMovimento;
        }
    }
}
