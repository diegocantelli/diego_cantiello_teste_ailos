using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class IdempotenciaQuery
    {
        private readonly DatabaseConfig _db;

        public IdempotenciaQuery(DatabaseConfig db)
        {
            _db = db;
        }

        public async Task<Idempotencia?> GetByChaveAsync(string chave)
        {
            using var connection = _db.CreateConnection();

            var sql = @"SELECT chave_idempotencia AS ChaveIdempotencia,
                               requisicao AS Requisicao,
                               resultado AS Resultado
                        FROM idempotencia
                        WHERE chave_idempotencia = @chave";

            return await connection.QueryFirstOrDefaultAsync<Idempotencia>(sql, new { chave });
        }
    }
}
