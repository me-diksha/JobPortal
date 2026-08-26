using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Repositories.Abstractions;

namespace JobPortalAPI.Repositories
{
    public class CommonRepository:ICommonRepository
    {
        private readonly IDbAccess _dbAccess;
        private readonly ILogger<CommonRepository> _logger;
        public CommonRepository(IDbAccess dbAccess,ILogger<CommonRepository> commonlogger) {

            _dbAccess = dbAccess;
            _logger = commonlogger;
           
        }

        public async Task<IEnumerable<Skills>> GetAllSkills()
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_all_skills();";


                using var connection =
                    _dbAccess.CreateConnection();


                return await connection.QueryAsync<Skills>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting skills list");
                throw;
            }
        }
        
    }
}
