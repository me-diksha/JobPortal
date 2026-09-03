using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Repositories.Abstractions;

namespace JobPortalAPI.Repositories
{
    public class CommonRepository:ICommonRepository
    {
       
        private readonly ILogger<CommonRepository> _logger;
        private readonly IDbExecutor _dbExecutor;
        public CommonRepository(IDbExecutor dbExecutor,ILogger<CommonRepository> commonlogger) {

            _dbExecutor = dbExecutor;
            _logger = commonlogger;
           
        }

        public async Task<IEnumerable<Skills>> GetAllSkills()
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_all_skills();";

                return await _dbExecutor.QueryAsync<Skills>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting skills list");
                throw;
            }
        }

        public async Task<IEnumerable<EmploymentType>> GetAllEmploymentType()
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_all_employmenttype();";

                return await _dbExecutor.QueryAsync<EmploymentType>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting skills list");
                throw;
            }
        }

    }
}
