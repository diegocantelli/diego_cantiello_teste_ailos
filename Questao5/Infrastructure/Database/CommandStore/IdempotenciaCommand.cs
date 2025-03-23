using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class IdempotenciaCommand
    {
        private readonly DatabaseConfig _db;

        public IdempotenciaCommand(DatabaseConfig db)
        {
            _db = db;
        }

        public async Task InserirAsync(Idempotencia idem)
        {
            using var connection = _db.CreateConnection();

            var sql = @"INSERT INTO idempotencia (
                            chave_idempotencia, requisicao, resultado)
                        VALUES (
                            @ChaveIdempotencia, @Requisicao, @Resultado);";

            await connection.ExecuteAsync(sql, idem);
        }
    }
}
