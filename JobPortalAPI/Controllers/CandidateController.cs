using JobPortalAPI.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace JobPortalAPI.Controllers
{
    [Authorize(Policy = PolicyNames.CandidateOnly)]
    [ApiController]
    [Route("api/candidate")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
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
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        #region[profile]

        [HttpPost("createprofile")]
        public async Task<IActionResult> CreateProfile(
        CreateCandidateProfileRequest request)
        {
            

            var result = await _candidateService.CreateProfile(
                UserId,
                request);

            return Ok(result);
        }

        [HttpGet("getprofile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _candidateService.GetProfile(UserId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("profile")]
        public async Task<IActionResult> GetFullProfile()
        {

            var response = await _candidateService.GetFullProfile(UserId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }
        [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteProfile(long userid)
        {
            var result = await _candidateService.DeleteProfile(userid);

            if (!result)
                return NotFound("Candidate profile not found");

            return Ok("Profile deleted successfully");
        }
        #endregion

        #region[skills]
        [HttpPost("addskills")]
        public async Task<IActionResult> AddSkill(
       CandidateSkillRequest request)
        {
            var result = await _candidateService.AddSkill(UserId,request);

            return Ok(result);
        }

        [HttpGet("getskills")]
        public async Task<IActionResult> GetSkills()
        {
            var result = await _candidateService.GetSkills(UserId);

            return Ok(result);
        }

        [HttpDelete("skills/{id}")]
        public async Task<IActionResult> DeleteSkill([FromRoute] int id)
        {
            var deleted=await _candidateService.DeleteSkill(id,UserId);
            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Skill not found"
                });
            }

            return Ok(new
            {
                Message = "Skill deleted successfully"
            });
        }

        #endregion

        #region[education]
        [HttpPost("addeducation")]
        public async Task<IActionResult> AddEducation(
        CandidateEducationRequest request)
        {
            var result = await _candidateService.AddEducation(UserId,request);

            return Ok(result);
        }

        [HttpGet("geteducation")]
        public async Task<IActionResult> GetEducation()
        {
            var result = await _candidateService.GetEducation(UserId);

            return Ok(result);
        }

        [HttpPut("updateeducation")]

        public async Task<IActionResult> UpdateEducation(CandidateEducationRequest request,int id)
        {
            var result = await _candidateService.UpdateEducation(UserId, request,id);

            return Ok(result);
        }

        [HttpDelete("education/{id}")]
        public async Task<IActionResult> DeleteEducation([FromRoute] int id)
        {
           
            var deleted =await _candidateService.DeleteEducation(id,UserId);
            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Education not found"
                });
            }

            return Ok(new
            {
                Message = "Education deleted successfully"
            });
        }

        #endregion

        #region[experience]
        [HttpPost("addexperience")]
        public async Task<IActionResult> AddExperience( 
        CandidateExperienceRequest request)
        {
            var result = await _candidateService.AddExperience(UserId,request);

            return Ok(result);
        }
        [HttpGet("getexperience")]
        public async Task<IActionResult> GetExperience()
        {
            var result = await _candidateService.GetExperience(UserId);

            return Ok(result);
        }

        [HttpDelete("experience/{id}")]
        public async Task<IActionResult> DeleteExperience([FromRoute] int id)
        {

            var deleted= await _candidateService.DeleteExperience(id,UserId);
            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "REQUIRED EXPERIENCE  NOT FOUND"
                });
            }

            return Ok(new
            {
                Message = "Experience deleted successfully"
            });
        }
        [HttpPut("updateexperience")]

        public async Task<IActionResult> UpdateExperience(CandidateExperienceRequest request,int id)
        {
            var result = await _candidateService.UpdateExperience(UserId, request,id);

            return Ok(result);
        }
        #endregion

    }
}
