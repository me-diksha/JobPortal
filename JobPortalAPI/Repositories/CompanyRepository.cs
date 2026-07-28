using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Repositories.Abstractions;

namespace JobPortalAPI.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbAccess _dbAccess;
        private readonly ILogger<CompanyRepository> _logger;


        public CompanyRepository(
            IDbAccess dbAccess,
            ILogger<CompanyRepository> logger)
        {
            _dbAccess = dbAccess;
            _logger = logger;
        }



        public async Task<long> CreateCompany(Company company)
        {
            try
            {
                const string sql = @"
                SELECT create_company
                (
                    @Name,
                    @Description,
                    @Industry,
                    @Website,
                    @Email,
                    @Phone,
                    @AddressLine1,
                    @AddressLine2,
                    @City,
                    @State,
                    @Country,
                    @PostalCode,
                    @LogosUrl,
                    @CompanySize,
                    @FoundedYear,
                    @CreatedBy
                );";


                using var connection =
                    _dbAccess.CreateConnection();


                return await connection.ExecuteScalarAsync<long>(
                    sql,
                    company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                throw;
            }
        }




        public async Task<Company?> GetCompany(long id)
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_company(@Id);";


                using var connection =
                    _dbAccess.CreateConnection();


                return await connection.QueryFirstOrDefaultAsync<Company>(
                    sql,
                    new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting company");
                throw;
            }
        }





        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            try
            {
                const string sql =
                    "SELECT * FROM get_all_companies();";


                using var connection =
                    _dbAccess.CreateConnection();


                return await connection.QueryAsync<Company>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting company list");
                throw;
            }
        }





        public async Task<long> UpdateCompany(Company company)
        {
            try
            {
                const string sql = @"
                SELECT update_company
                (
                    @Id,
                    @Name,
                    @Description,
                    @Industry,
                    @Website,
                    @UpdatedBy
                );";


                using var connection =
                    _dbAccess.CreateConnection();


                return await connection.ExecuteScalarAsync<long>(
                    sql,
                    company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company");
                throw;
            }
        }





        public async Task<bool> DeleteCompany(long id, long updatedBy)
        {
            try
            {
                const string sql =
                    "SELECT delete_company(@Id,@UpdatedBy);";


                using var connection =
                    _dbAccess.CreateConnection();


                return await connection.ExecuteScalarAsync<bool>(
                    sql,
                    new
                    {
                        Id = id,
                        UpdatedBy = updatedBy
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company");
                throw;
            }
        }
    }
}