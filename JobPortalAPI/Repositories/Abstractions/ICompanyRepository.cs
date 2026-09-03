using JobPortalAPI.Models.Common;

namespace JobPortalAPI.Repositories.Abstractions
{
    public interface ICompanyRepository
    {
        Task<long> CreateCompany(Company company);

        Task<Company?> GetCompany(long id);

        Task<IEnumerable<Company>> GetAllCompanies();

        Task<Company> UpdateCompany(Company company);

        Task<bool> DeleteCompany(long id, long updatedBy);
    }
}