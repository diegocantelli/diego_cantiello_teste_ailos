using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Command;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentoCommand : IMovimentoCommand
    {
        private readonly DatabaseConfig _db;

        public MovimentoCommand(DatabaseConfig db)
        {
            _db = db;
        }

        public async Task InserirAsync(Movimento movimento)
        {
            using var connection = _db.CreateConnection();

            var sql = @"INSERT INTO movimento (
                            idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                        VALUES (
                            @IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);";

            await connection.ExecuteAsync(sql, movimento);
        }
    }
}
