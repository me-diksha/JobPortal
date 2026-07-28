using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Repositories.Abstractions;

namespace JobPortalAPI.Repositories
{
    public class RecruiterRepository:IRecruiterRepository
    {
        private readonly IDbAccess _dbAccess;
        private readonly ILogger<RecruiterRepository> _logger;

        public RecruiterRepository(
            IDbAccess dbAccess,
            ILogger<RecruiterRepository> logger)
        {
            _dbAccess = dbAccess;
            _logger = logger;
        }
       
            public async Task<long> CreateProfile(Recruiter recruiter)
            {
                try
                {
                    const string sql = @"
                    SELECT create_recruiter
                    (
                      @UserId,
                      @CompanyId,
                      @FirstName,
                      @LastName,
                      @Designation,
                      @Department,
                      @Phone,
                      @LinkedinUrl
                    );";

                    using var connection = _dbAccess.CreateConnection();

                    return await connection.ExecuteScalarAsync<long>(
                        sql,
                        recruiter);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating recruiter profile");
                    throw;
                }
            }

            public async Task<Recruiter?> GetProfile(int userId)
            {
                try
                {
                    const string sql =
                        "SELECT * FROM get_recruiter(@UserId);";

                    using var connection =
                        _dbAccess.CreateConnection();

                    return await connection.QueryFirstOrDefaultAsync<Recruiter>(
                        sql,
                        new { UserId = userId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting recruiter profile");
                    throw;
                }
            }

            public async Task<long> UpdateProfile(Recruiter recruiter)
            {
                try
                {
                    const string sql = @"
                        SELECT update_recruiter
                        (
                            @UserId,
                            @CompanyId,
                            @FirstName,
                            @LastName,
                            @Designation,
                            @Department,
                            @Phone,
                            @LinkedinUrl
                        );";

                    using var connection =
                        _dbAccess.CreateConnection();

                    return await connection.ExecuteScalarAsync<long>(
                        sql,
                        recruiter);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating recruiter");
                    throw;
                }
            }

            public async Task<bool> DeleteProfile(int userId)
            {
                try
                {
                    const string sql =
                        "SELECT delete_recruiter(@UserId);";

                    using var connection =
                        _dbAccess.CreateConnection();

                    return await connection.ExecuteScalarAsync<bool>(
                        sql,
                        new { UserId = userId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting recruiter");
                    throw;
                }
            }
        }

    }

