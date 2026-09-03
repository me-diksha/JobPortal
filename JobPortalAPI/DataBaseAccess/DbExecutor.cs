using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;

namespace JobPortalAPI.DataBaseAccess
{
    public class DbExecutor : IDbExecutor
    {
        private readonly IDbAccess _dbAccess;

        public DbExecutor(IDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public async Task<T> ExecuteScalarAsync<T>(
            string sql,
            object? parameters = null)
        {
            using var connection =
                _dbAccess.CreateConnection();

            return await connection.ExecuteScalarAsync<T>(
                sql,
                parameters);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object? parameters = null)
        {
            using var connection =
                _dbAccess.CreateConnection();

            return await connection.QueryAsync<T>(
                sql,
                parameters);
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? parameters = null)
        {
            using var connection =
                _dbAccess.CreateConnection();

            return await connection
                .QueryFirstOrDefaultAsync<T>(
                    sql,
                    parameters);
        }

        public async Task<T> QuerySingleAsync<T>(
            string sql,
            object? parameters = null)
        {
            using var connection =
                _dbAccess.CreateConnection();

            return await connection.QuerySingleAsync<T>(
                sql,
                parameters);
        }
    }
}