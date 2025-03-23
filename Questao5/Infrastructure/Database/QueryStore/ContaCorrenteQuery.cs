using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Query;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class ContaCorrenteQuery : IContaCorrenteQuery
    {
        private readonly DatabaseConfig _dbConfig;

        //public ContaCorrenteQuery(){}

        public ContaCorrenteQuery(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<ContaCorrente?> GetByNumeroAsync(int numero)
        {
            using var connection = _dbConfig.CreateConnection();

            var sql = @"SELECT idcontacorrente AS IdContaCorrente,
                               numero,
                               nome,
                               ativo
                        FROM contacorrente
                        WHERE numero = @numero";

            return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { numero });
        }

        public async Task<ContaCorrente?> GetByIdAsync(string id)
        {
            using var connection = _dbConfig.CreateConnection();

            var sql = @"SELECT idcontacorrente AS IdContaCorrente,
                               numero,
                               nome,
                               ativo
                        FROM contacorrente
                        WHERE idcontacorrente = @id";

            return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { id });
        }
    }
}
