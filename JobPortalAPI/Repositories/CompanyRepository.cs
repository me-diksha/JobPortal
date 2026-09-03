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

        private readonly ILogger<CompanyRepository> _logger;
        private readonly IDbExecutor _dbExecutor;


        public CompanyRepository(ILogger<CompanyRepository> logger, IDbExecutor dbExecutor)
        {

            _logger = logger;
            _dbExecutor = dbExecutor;
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



                return await _dbExecutor.ExecuteScalarAsync<long>(
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


                return await _dbExecutor.QueryFirstOrDefaultAsync<Company>(
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


                return await _dbExecutor.QueryAsync<Company>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting company list");
                throw;
            }
        }





        public async Task<Company> UpdateCompany(Company company)
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



                return await _dbExecutor.QuerySingleAsync<Company>(
                    sql,
                    new
                    {
                        Name = company.Name,
                        Description = company.Description,
                        Industry = company.Industry,
                        Website = company.Website,
                        Email = company.Email,
                        Phone = company.Phone,
                        Addressline1 = company.AddressLine1,
                        Addressline2 = company.AddressLine2,
                        City = company.City,
                        State = company.State,
                        Country = company.Country,
                        Postalcode = company.PostalCode,
                        Logosurl = company.LogosUrl,
                        CompanySize = company.CompanySize,
                        FoundedYear = company.FoundedYear,
                        UpdatedBy = company.UpdatedBy,
                        CompanyId = company.Id
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


                return await _dbExecutor.ExecuteScalarAsync<bool>(
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