using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;

namespace JobPortalAPI.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly ILogger<JobRepository> _logger;

        public JobRepository(
            IDbExecutor dbExecutor,
            ILogger<JobRepository> logger)
        {
            _dbExecutor = dbExecutor;
            _logger = logger;
        }

        public async Task<long> CreateJob(Job job)
        {
            try
            {
                const string sql = @"
                    SELECT create_job
                    (
                        @CompanyId,
                        @Title,
                        @Description,
                        @Location,
                        @RefEmploymentType,
                        @RefExperienceLevel,
                        @MinSalary,
                        @MaxSalary,
                        @Deadline,
                        @CreatedBy
                    );";


                return await _dbExecutor.ExecuteScalarAsync<long>(
                    sql,
                    job);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating job");
                throw;
            }
        }

        public async Task<JobResponse?> GetJob(long id)
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_job(@Id);";

                return await _dbExecutor.QueryFirstOrDefaultAsync<JobResponse>(
                    sql,
                    new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting job");
                throw;
            }
        }

        public async Task<IEnumerable<JobResponse>> GetAllJobs()
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_all_jobs();";


                return await _dbExecutor.QueryAsync<JobResponse>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all jobs");
                throw;
            }
        }

        public async Task<IEnumerable<JobResponse>> GetRecruiterJobs(
            long companyId)
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_recruiter_jobs(@CompanyId);";

                
                return await _dbExecutor.QueryAsync<JobResponse>(
                    sql,
                    new { CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error getting recruiter jobs");

                throw;
            }
        }

        public async Task<JobResponse> UpdateJob(Job job)
        {
            try
            {
                const string sql = @"
                    SELECT *
                    FROM update_job
                    (
                        @Id,
                        @CompanyId,
                        @Title,
                        @Description,
                        @Location,
                        @RefEmploymentType,
                        @RefExperienceLevel,
                        @MinSalary,
                        @MaxSalary,
                        @Deadline,
                        @UpdatedBy
                    );";

                return await _dbExecutor.QuerySingleAsync<JobResponse>(
                    sql,
                    job);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating job");
                throw;
            }
        }

        public async Task<bool> DeleteJob(
            long id,
            long companyId,
            long updatedBy)
        {
            try
            {
                const string sql =
                    "SELECT delete_job(@Id, @CompanyId, @UpdatedBy);";


                return await _dbExecutor.ExecuteScalarAsync<bool>(
                    sql,
                    new
                    {
                        Id = id,
                        CompanyId = companyId,
                        UpdatedBy = updatedBy
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting job");
                throw;
            }
        }
    }
}