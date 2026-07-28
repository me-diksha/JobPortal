using JobPortalAPI.Models.Common;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;

namespace JobPortalAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CompanyService> _logger;


        public CompanyService(
            ICompanyRepository companyRepository,
            ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }




        public async Task<long> CreateCompany(Company company)
        {
            try
            {
                if (company == null)
                    throw new ArgumentNullException(nameof(company));


                if (string.IsNullOrWhiteSpace(company.Name))
                    throw new ArgumentException(
                        "Company name is required");


                return await _companyRepository.CreateCompany(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                throw;
            }
        }





        public async Task<Company?> GetCompany(long id)
        {
            if (id <= 0)
                return null;


            return await _companyRepository.GetCompany(id);
        }





        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _companyRepository.GetAllCompanies();
        }





        public async Task<long> UpdateCompany(Company company)
        {
            try
            {
                if (company == null)
                    throw new ArgumentNullException(nameof(company));


                if (company.Id <= 0)
                    throw new ArgumentException(
                        "Invalid company id");


                return await _companyRepository.UpdateCompany(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company");
                throw;
            }
        }





        public async Task<bool> DeleteCompany(
            long id,
            long updatedBy)
        {
            if (id <= 0)
                return false;


            return await _companyRepository.DeleteCompany(
                id,
                updatedBy);
        }
    }
}