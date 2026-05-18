using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;

namespace JobPortalAPI.DataAccess
{
    public class AuthRepository:IAuthRepository
    {
        private readonly IDbAccess _dbAccess;
        private readonly ILogger<AuthRepository> _logger;
        public AuthRepository(IDbAccess dbAccess, ILogger<AuthRepository> logger) { 
         _dbAccess = dbAccess;
         _logger = logger;
        }
        public async Task<User ?> GetUserByEmail(string email)
        {
            const string query = "SELECT * FROM GetUserByEmail(@Email); ";
            using var connection = _dbAccess.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
            query,
            new
            {
                Email = email
            });

        }
       public async Task<long> CreateUser(User user)
        {

            const string query = "SELECT CreateUser(@Name,  @Email, @PasswordHash, @RoleId);";
            using var connection = _dbAccess.CreateConnection();

            return await connection.ExecuteScalarAsync<long>(
                query,
                new
                {
                    user.Name,
                    user.Email,
                    user.PasswordHash,
                    user.RoleId
                });
        }

    }
}
