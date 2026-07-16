using Dapper;
using JobPortalAPI.Common;
using JobPortalAPI.DataAccess;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
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

        #region[profile]
        public async Task<int> CreateProfile(CandidateProfile request)
        {
            try
            {
                const string sql = "SELECT createprofile(@UserId,@Headline,@Bio,@CurrentSalary,@ExpectedSalary,@ResumeUrl,@AddressLine1,@AddressLine2,@City,@State,@Country,@Firstname,@LastName);";
                using var connection = _dbAccess.CreateConnection();
                var affectedRows = await connection.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        UserId = request.UserId,
                        Headline = request.Headline,
                        Bio = request.Bio,
                        CurrentSalary = request.CurrentSalary,
                        ExpectedSalary = request.ExpectedSalary,
                        ResumeUrl = request.ResumeUrl,
                        AddressLine1 = request.AddressLine1,
                        AddressLine2 = request.AddressLine2,
                        City = request.City,
                        State = request.State,
                        Country = request.Country,
                        Firstname = request.Firstname,
                        LastName = request.LastName
                    });

                return affectedRows;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in creating profile");
                throw;
            }
        }

        public async Task<CandidateProfile?> GetProfile(int userId)
        {
            try
            {
                const string sql = "SELECT * FROM get_candidateprofile(@UserId);";

                using var connection =
                    _dbAccess.CreateConnection();

                return await connection.QueryFirstOrDefaultAsync<CandidateProfile>(
                    sql,
                    new { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Getprofile");
                throw;
            }
        }

        #endregion

        #region[skill]
        public async Task<List<CandidateSkill>> GetSkills(int userId)
        {
            try {
                const string sql = "SELECT * FROM get_candidate_skills(@UserId);";

                using var connection =
                    _dbAccess.CreateConnection();

                var result =
                    await connection.QueryAsync<CandidateSkill>(
                        sql,
                        new { UserId = userId });

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSkills");
                throw;
            }
        }
        public async Task<int> AddSkill(CandidateSkill skill)
        {
            try
            {
                const string sql = @"
            SELECT add_candidate_skill
              (
            @UserId,
            @SkillId,
            @ExperienceYears
             );";

                using var connection = _dbAccess.CreateConnection();

                return await connection.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        UserId = skill.UserId,
                        SkillId = skill.SkillId,
                        ExperienceYears = skill.ExperienceYears
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in addingSkills");
                throw;
            }
        }
        public async Task<bool> DeleteSkill(int skillRecordId, int userId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteSkill");
                throw;
            }
        }

        #endregion

        #region[education]
        public async Task<List<CandidateEducation>> GetEducation(int userId)
        {
            try
            {
                const string sql = "SELECT * FROM get_candidate_education(@UserId);";

                using var connection =
                    _dbAccess.CreateConnection();

                var result =
                    await connection.QueryAsync<CandidateEducation>(
                        sql,
                        new { UserId = userId });

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEducation");
                throw;
            }
        }
        public async Task<CandidateEducation> UpdateEducation(CandidateEducation education, int educationid)
        {
            try
            {
                const string sql = @"
                SELECT * FROM updateeducation(
                    @UserId,
                    @InstituteName,
                    @Degree,
                    @FieldOfStudy,
                    @StartYear,
                    @EndYear,
                    @Percentage,
                    @EducationId
            );";
                using var connection = _dbAccess.CreateConnection();

                return await connection.QuerySingleAsync<CandidateEducation>(
                    sql,
                    new
                    {
                        UserId = education.UserId,
                        InstituteName = education.InstituteName,
                        FieldOfStudy = education.FieldOfStudy,
                        StartYear = education.StartYear,
                        EndYear = education.EndYear,
                        Degree = education.Degree,
                        Percentage = education.Percentage,
                        EducationId=educationid
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in updating educcation");
                throw;
            }
        }
        public async Task<int> AddEducation(CandidateEducation education)
        {
            try
            {
                const string sql = @"
            SELECT add_candidate_education
              (
            @UserId,
            @InstituteName,
            @Degree,
            @FieldOfStudy,
            @StartYear,
            @EndYear,
            
            @Percentage

             );";

                using var connection = _dbAccess.CreateConnection();

                return await connection.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        UserId = education.UserId,
                        InstituteName = education.InstituteName,
                        FieldOfStudy = education.FieldOfStudy,
                        StartYear = education.StartYear,
                        EndYear = education.EndYear,
                        Degree = education.Degree,
                        Percentage = education.Percentage
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddEducation");
                throw;
            }
        }
        public async Task<bool> DeleteEducation(int educationId, int userId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteEducation");
                throw;
            }
        }
        #endregion

        #region[Experience]
        public async Task<List<CandidateExperience>> GetExperience(int userId)
        {
            try
            {
                const string sql = "SELECT * FROM get_candidate_experience(@UserId);";

                using var connection =
                    _dbAccess.CreateConnection();

                var result =
                    await connection.QueryAsync<CandidateExperience>(
                        sql,
                        new { UserId = userId });

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetExperience");
                throw;
            }
        }




        public async Task<int> AddExperience(CandidateExperience experience)
        {
            try
            {
                const string sql = @"
            SELECT  add_candidate_experience
              (
                    @UserId ,
                    @CompanyName,
                    @Designation ,
                    @StartDate,
                    @EndDate,
                    @CurrentlyWorking ,
                    @Description 

             );";

                using var connection = _dbAccess.CreateConnection();

                return await connection.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        UserId = experience.UserId,
                        CompanyName = experience.CompanyName,
                        Designation = experience.Designation,
                        StartDate = experience.StartDate,
                        EndDate = experience.EndDate,
                        CurrentlyWorking = experience.CurrentlyWorking,
                        Description = experience.Description
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in addingExperience");
                throw;
            }
        }
       public async Task<CandidateExperience> UpdateExperience(CandidateExperience experience)
        {
            try
            {
                const string sql = @"
        SELECT *
        FROM update_candidate_experience
        (
            @Id,
            @UserId,
            @CompanyName,
            @Designation,
            @StartDate,
            @EndDate,
            @CurrentlyWorking,
            @Description
        );";

                using var connection = _dbAccess.CreateConnection();

                return await connection.QuerySingleAsync<CandidateExperience>(
                    sql,
                    new
                    {
                        Id = experience.Id,
                        UserId = experience.UserId,
                        CompanyName = experience.CompanyName,
                        Designation = experience.Designation,
                        StartDate = experience.StartDate,
                        EndDate = experience.EndDate,
                        CurrentlyWorking = experience.CurrentlyWorking,
                        Description = experience.Description
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating experience");
                throw;
            }
        }


        public async Task<bool> DeleteExperience(int experienceId, int userId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteExperience");
                throw;
            }
        }
        #endregion
    }
}
