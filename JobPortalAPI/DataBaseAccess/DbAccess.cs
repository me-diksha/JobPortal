using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Settings;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace JobPortalAPI.DataBaseAccess
{
    public class DbAccess :IDbAccess
    {
        private readonly string _connectionString;

        public DbAccess(IOptions<DBSettings> dbOptions)
        {
            var db = dbOptions.Value;

            _connectionString = new NpgsqlConnectionStringBuilder
            {
                Host = db.Host,
                Port = db.Port ?? 5432,
                Database = db.DatabaseName,
                Username = db.Username,
                Password = db.Password,
                Pooling = true,
                MinPoolSize = 1,
                MaxPoolSize = 100,
            }.ToString();
        }
        public IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}