using JobPortalAPI.Models.Common;

namespace JobPortalAPI.Services.Abstractions
{
    public interface ICompanyService
    {
        Task<long> CreateCompany(Company company);

        Task<Company?> GetCompany(long id);

        Task<IEnumerable<Company>> GetAllCompanies();

        Task<Company> UpdateCompany(Company company);

        Task<bool> DeleteCompany(long id, long updatedBy);
    }
}