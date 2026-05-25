using Dapper;
using JobPortalAPI.DataAccess;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;

namespace JobPortalAPI.Repositories
{
    public class CandidateRepository :ICandidateRepository
    {
        private readonly IDbAccess _dbAccess;
        private readonly ILogger<CandidateRepository> _logger;
        public CandidateRepository(IDbAccess  dbAccess, ILogger<CandidateRepository> logger) { 
            _dbAccess = dbAccess;
            _logger  = logger;
        }

        public async Task<CandidateProfile?> GetProfile(int userId)
        {
            const string sql = @"
           SELECT *
           FROM candidate_profiles
           WHERE userid = @UserId";

            using var connection =
                _dbAccess.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<CandidateProfile>(
                sql,
                new { UserId = userId });
        }
        public async Task<List<CandidateSkillResponse>> GetSkills(int userId)
        {
            const string sql = @"
            SELECT
            cs.id,
            cs.skillid,
            s.name AS SkillName,
            cs.experienceyears
            FROM candidate_skills cs
            INNER JOIN skills s
            ON s.id = cs.skillid
            WHERE cs.userid = @UserId";

            using var connection =
                _dbAccess.CreateConnection();

            var result =
                await connection.QueryAsync<CandidateSkillResponse>(
                    sql,
                    new { UserId = userId });

            return result.ToList();
        }
        public async Task<List<CandidateEducation>> GetEducation(int userId)
        {
            const string sql = @"
            SELECT *
            FROM candidate_education
            WHERE userid = @UserId";

            using var connection =
                _dbAccess.CreateConnection();

            var result =
                await connection.QueryAsync<CandidateEducation>(
                    sql,
                    new { UserId = userId });

            return result.ToList();
        }
        public async Task<List<CandidateExperience>> GetExperience(int userId)
        {
            const string sql = @"
            SELECT *
            FROM candidate_experience
            WHERE userid = @UserId";

            using var connection =
                _dbAccess.CreateConnection();

            var result =
                await connection.QueryAsync<CandidateExperience>(
                    sql,
                    new { UserId = userId });

            return result.ToList();
        }

        public async Task<bool> DeleteSkill(int skillRecordId, int userId)
        {
            const string sql = @"
            DELETE FROM candidate_skills
            WHERE id = @SkillRecordId
            AND userid = @UserId;";

            using var connection = _dbAccess.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(
                sql,
                new
                {
                    SkillRecordId = skillRecordId,
                    UserId = userId
                });

            return affectedRows > 0;
        }

        public async Task<bool> DeleteEducation(int educationId, int userId)
        {
            const string sql = @"
            DELETE FROM candidate_education
            WHERE id = @EducationId
            AND userid = @UserId;";

            using var connection = _dbAccess.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(
                sql,
                new
                {
                    EducationId = educationId,
                    UserId = userId
                });

            return affectedRows > 0;
        }
        public async Task<bool> DeleteExperience(int experienceId, int userId)
        {
            const string sql = @"
            DELETE FROM candidate_experience
            WHERE id = @experienceId
            AND userid = @UserId;";

            using var connection = _dbAccess.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(
                sql,
                new
                {
                    ExperienceId = experienceId,
                    UserId = userId
                });

            return affectedRows > 0;
        }
    }
}
