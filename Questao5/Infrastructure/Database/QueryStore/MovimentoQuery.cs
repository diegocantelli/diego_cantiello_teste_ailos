using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class MovimentoQuery
    {
        private readonly DatabaseConfig _db;

        public MovimentoQuery(DatabaseConfig db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Movimento>> GetAllByContaIdAsync(string idContaCorrente)
        {
            using var connection = _db.CreateConnection();

            var sql = @"SELECT idmovimento AS IdMovimento,
                               idcontacorrente AS IdContaCorrente,
                               datamovimento AS DataMovimento,
                               tipomovimento AS TipoMovimento,
                               valor
                        FROM movimento
                        WHERE idcontacorrente = @idContaCorrente";

            return await connection.QueryAsync<Movimento>(sql, new { idContaCorrente });
        }

        public async Task<Movimento?> GetByIdAsync(string idMovimento)
        {
            using var connection = _db.CreateConnection();

            var sql = @"SELECT idmovimento AS IdMovimento,
                               idcontacorrente AS IdContaCorrente,
                               datamovimento AS DataMovimento,
                               tipomovimento AS TipoMovimento,
                               valor
                        FROM movimento
                        WHERE idmovimento = @idMovimento";

            return await connection.QueryFirstOrDefaultAsync<Movimento>(sql, new { idMovimento });
        }
    }
}
