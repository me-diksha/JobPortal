using Dapper;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Repositories.Abstractions;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

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





        public async Task<Company>UpdateCompany(Company company)
        {
            try
            {
                const string sql = @"
                SELECT update_company
                (
                            @Name,
                            @Description,
                            @Industry,
                            @Website,
                            @Email,
                            @Phone,
                            @Addressline1,
                            @Addressline2,
                            @City,
                            @State,
                            @Country,
                            @Postalcode,
                            @Logosurl,
                            @CompanySize,
                            @FoundedYear,
                            @UpdatedBy,
                            @CompanyId
                );";


                using var connection =
                    _dbAccess.CreateConnection();


                return await connection.QuerySingleAsync<Company>(
                    sql,
                    new {
                        Name=company.Name,
                        Description=company.Description,
                        Industry=company.Industry,
                        Website=company.Website,
                        Email=company.Email,
                        Phone=company.Phone,
                        Addressline1=company.AddressLine1,
                        Addressline2=company.AddressLine2,
                        City=company.City,
                        State=company.State,
                        Country=company.Country,
                        Postalcode=company.PostalCode,
                        Logosurl=company.LogosUrl,
                        CompanySize=company.CompanySize,
                        FoundedYear=company.FoundedYear,
                        UpdatedBy=company.UpdatedBy,
                        CompanyId=company.Id
                    });
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