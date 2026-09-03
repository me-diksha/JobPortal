using JobPortalAPI.Common;
using JobPortalAPI.Services.Abstractions;
using JobPortalAPI.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("all/skills")]
        public async Task<IActionResult> GetAllSkills()
        {
            var result = await _commonService.GetAllSkills();

            return Ok(result);
        }
        [HttpGet("all/employmentType")]
        public async Task<IActionResult> GetAllEmploymentType()
        {
            var result = await _commonService.GetAllEmploymentType();
            return Ok(result);
        }
    }
}
