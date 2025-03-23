using FluentAssertions;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Command;
using Questao5.Domain.Interfaces.Query;
using Questao5.Domain.Language;
using Xunit;

namespace Questao5.Tests.Application.Commands
{
    public class CriarMovimentoCommandHandlerTests
    {
        private readonly CriarMovimentoCommandHandler _handler;
        private readonly IContaCorrenteQuery _contaQuery = Substitute.For<IContaCorrenteQuery>();
        private readonly IMovimentoCommand _movimentoCommand = Substitute.For<IMovimentoCommand>();
        private readonly IIdempotenciaCommand _idempotenciaCommand = Substitute.For<IIdempotenciaCommand>();
        private readonly IIdempotenciaQuery _idempotenciaQuery = Substitute.For<IIdempotenciaQuery>();

        public CriarMovimentoCommandHandlerTests()
        {
            _handler = new CriarMovimentoCommandHandler(
                _contaQuery,
                _movimentoCommand,
                _idempotenciaCommand,
                _idempotenciaQuery
            );
        }

        [Fact]
        public async Task Deve_Retornar_IdIdempotente_Quando_Movimento_Ja_Criado()
        {
            var command = new CriarMovimentoCommand
            {
                IdRequisicao = "REQ123",
                IdContaCorrente = "ID1",
                Valor = 100,
                TipoMovimento = "C"
            };

            _idempotenciaQuery.GetByChaveAsync(command.IdRequisicao)
                .Returns(new Idempotencia
                {
                    ChaveIdempotencia = command.IdRequisicao,
                    Resultado = "abc123"
                });

            var result = await _handler.Handle(command, default);

            result.Should().Be("abc123");
        }

        [Fact]
        public async Task Should_Throw_When_ContaCorrente_Not_Found()
        {
            var command = new CriarMovimentoCommand
            {
                IdRequisicao = "REQ456",
                IdContaCorrente = "NAO_EXISTE",
                Valor = 100,
                TipoMovimento = "D"
            };

            _idempotenciaQuery.GetByChaveAsync(command.IdRequisicao).Returns((Idempotencia?)null);
            _contaQuery.GetByIdAsync(command.IdContaCorrente).Returns((ContaCorrente?)null);

            var act = async () => await _handler.Handle(command, default);

            await act.Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.Type == "INVALID_ACCOUNT");
        }
    }
}
