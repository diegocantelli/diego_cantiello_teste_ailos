﻿using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Constants;
using Questao5.Domain.Interfaces.Query;
using Questao5.Domain.Language;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoQueryHandler : IRequestHandler<ConsultarSaldoQuery, ConsultarSaldoResponse>
    {
        private readonly IContaCorrenteQuery _contaCorrenteQuery;
        private readonly IMovimentoQuery _movimentoQuery;

        public ConsultarSaldoQueryHandler(
            IContaCorrenteQuery contaCorrenteQuery,
            IMovimentoQuery movimentoQuery)
        {
            _contaCorrenteQuery = contaCorrenteQuery;
            _movimentoQuery = movimentoQuery;
        }

        public async Task<ConsultarSaldoResponse> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            var conta = await _contaCorrenteQuery.GetByIdAsync(request.IdContaCorrente);

            if (conta == null)
                throw new BusinessException("Conta corrente inválida", ErrosNegocio.ContaInvalida);

            if (!conta.Ativo)
                throw new BusinessException("Conta corrente inativa", ErrosNegocio.ContaInativa);

            conta.Movimentos = await _movimentoQuery.GetAllByContaIdAsync(request.IdContaCorrente);

            return new ConsultarSaldoResponse
            {
                Numero = conta.Numero,
                Nome = conta.Nome,
                DataResposta = DateTime.Now,
                Saldo = conta.Saldo()
            };
        }
    }
}
