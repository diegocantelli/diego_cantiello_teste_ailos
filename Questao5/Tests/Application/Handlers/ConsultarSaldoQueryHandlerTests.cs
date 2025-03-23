using FluentAssertions;
using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Constants;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Query;
using Questao5.Domain.Language;
using Xunit;

namespace Questao5.Tests.Application.Handlers
{
    public class ConsultarSaldoQueryHandlerTests
    {
        private readonly IContaCorrenteQuery _contaQuery = Substitute.For<IContaCorrenteQuery>();
        private readonly IMovimentoQuery _movimentoQuery = Substitute.For<IMovimentoQuery>();
        private readonly ConsultarSaldoQueryHandler _handler;

        public ConsultarSaldoQueryHandlerTests()
        {
            _handler = new ConsultarSaldoQueryHandler(_contaQuery, _movimentoQuery);
        }

        [Fact]
        public async Task Deve_Retornar_Excecao_Para_Conta_Invalida()
        {
            var query = new ConsultarSaldoQuery { IdContaCorrente = "ID-INVALIDO" };

            _contaQuery.GetByIdAsync(query.IdContaCorrente).Returns((ContaCorrente?)null);

            var act = async () => await _handler.Handle(query, default);

            await act.Should()
                .ThrowAsync<BusinessException>()
                .Where(ex => ex.Type == ErrosNegocio.ContaInvalida);
        }

        [Fact]
        public async Task Deve_Retornar_Excecao_Para_Conta_Inativa()
        {
            var query = new ConsultarSaldoQuery { IdContaCorrente = "INATIVA" };

            _contaQuery.GetByIdAsync(query.IdContaCorrente).Returns(new ContaCorrente
            {
                IdContaCorrente = query.IdContaCorrente,
                Nome = "Inativa",
                Numero = 456,
                Ativo = false
            });

            var act = async () => await _handler.Handle(query, default);

            await act.Should()
                .ThrowAsync<BusinessException>()
                .Where(ex => ex.Type == "INACTIVE_ACCOUNT");
        }

        [Fact]
        public async Task Deve_Retornar_Saldo_Zero_Para_Conta_Sem_Movimentos()
        {
            var query = new ConsultarSaldoQuery { IdContaCorrente = "SEM-MOV" };

            _contaQuery.GetByIdAsync(query.IdContaCorrente).Returns(new ContaCorrente
            {
                IdContaCorrente = query.IdContaCorrente,
                Nome = "Sem Movimento",
                Numero = 789,
                Ativo = true
            });

            _movimentoQuery.GetAllByContaIdAsync(query.IdContaCorrente)
                           .Returns(Enumerable.Empty<Movimento>());

            var result = await _handler.Handle(query, default);

            result.Saldo.Should().Be(0.00m);
        }

        [Fact]
        public async Task Deve_Calcular_Saldo_Para_Conta_Com_Movimento()
        {
            var query = new ConsultarSaldoQuery { IdContaCorrente = "COM-MOV" };

            _contaQuery.GetByIdAsync(query.IdContaCorrente).Returns(new ContaCorrente
            {
                IdContaCorrente = query.IdContaCorrente,
                Nome = "Com Movimento",
                Numero = 321,
                Ativo = true
            });

            _movimentoQuery.GetAllByContaIdAsync(query.IdContaCorrente)
                           .Returns(new List<Movimento>
                           {
                               new Movimento { TipoMovimento = 'C', Valor = 500 },
                               new Movimento { TipoMovimento = 'D', Valor = 100 },
                               new Movimento { TipoMovimento = 'D', Valor = 50 }
                           });

            var result = await _handler.Handle(query, default);

            result.Saldo.Should().Be(350.00m);
            result.Numero.Should().Be(321);
            result.Nome.Should().Be("Com Movimento");
        }
    }
}
