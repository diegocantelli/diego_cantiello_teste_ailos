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
        public async Task Deve_Criar_Novo_Movimento_Quando_Requisicao_Valida()
        {
            var command = new CriarMovimentoCommand
            {
                IdRequisicao = "REQ-FIRST",
                IdContaCorrente = "123-CONTA",
                Valor = 150.75m,
                TipoMovimento = 'C'
            };

            var contaCorrente = new ContaCorrente
            {
                IdContaCorrente = command.IdContaCorrente,
                Numero = 123,
                Nome = "Usuário Teste",
                Ativo = true
            };

            _idempotenciaQuery.GetByChaveAsync(command.IdRequisicao).Returns((Idempotencia?)null);
            _contaQuery.GetByIdAsync(command.IdContaCorrente).Returns(contaCorrente);

            string? idIdempotenciaCriado = null;
            _movimentoCommand.When(x => x.InserirAsync(Arg.Do<Movimento>(m => idIdempotenciaCriado = m.IdMovimento)))
                             .Do(_ => { });

            _idempotenciaCommand.When(x => x.InserirAsync(Arg.Any<Idempotencia>()))
                                .Do(_ => { });

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNullOrEmpty();
            result.Should().Be(idIdempotenciaCriado);
        }

        [Fact]
        public async Task Deve_Retornar_IdIdempotente_Quando_Movimento_Ja_Criado()
        {
            var command = new CriarMovimentoCommand
            {
                IdRequisicao = "REQ123",
                IdContaCorrente = "ID1",
                Valor = 100,
                TipoMovimento = 'C'
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
        public async Task Deve_Lancar_Excecao_Quando_Conta_Nao_Encontrada()
        {
            var command = new CriarMovimentoCommand
            {
                IdRequisicao = "REQ456",
                IdContaCorrente = "NAO_EXISTE",
                Valor = 100,
                TipoMovimento = 'D'
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
