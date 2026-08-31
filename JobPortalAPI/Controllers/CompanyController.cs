using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Request;
using JobPortalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPortalAPI.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;
        private int UserId
        {
            get
            {
                var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(claim))
                    throw new UnauthorizedAccessException();

                return int.Parse(claim);
            }
        }
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("createcompany")]
        public async Task<IActionResult> CreateCompany(
            [FromBody] Company company)
        {
            company.CreatedBy = UserId;
            var result =
                await _companyService.CreateCompany(company);


            return Ok(new
            {
                message = "Company created successfully",
                id = result
            });
        }


        [HttpGet("getcompany")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company =
                await _companyService.GetCompany(id);


            if (company == null)
                return NotFound(
                    "Company not found");


            return Ok(company);
        }
        [HttpGet("getallcompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies =
                await _companyService.GetAllCompanies();


            return Ok(companies);
        }

        [HttpDelete("deletecompany")]
        public async Task<IActionResult> DeleteCompany(
            long id,
            [FromQuery] long updatedBy)
        {
            var result =
                await _companyService.DeleteCompany(
                    id,
                    updatedBy);


            if (!result)
                return NotFound(
                    "Company not found");


            return Ok(new
            {
                message = "Company deleted successfully"
            });
        }
        [HttpPut("updateCompany")]
        public async Task<IActionResult> UpdateCompany(
            [FromBody] Company company)
        {
            var result =
                await _companyService.UpdateCompany(company);


            return Ok(new
            {
                message = "Company updated successfully",
                id = result
            });
        }

    }
}
