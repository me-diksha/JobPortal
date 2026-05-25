using JobPortalAPI.Models.Requests;
using JobPortalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
namespace JobPortalAPI.Controllers
{
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        [HttpPost("profile")]
        public async Task<IActionResult> CreateProfile(
        CreateCandidateProfileRequest request)
        {
            var result = await _candidateService.CreateProfile(request);

            return Ok(result);
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var result = await _candidateService.GetProfile(userId);

            return Ok(result);
        }

        [HttpPost("skills")]
        public async Task<IActionResult> AddSkill(
       CandidateSkillRequest request)
        {
            var result = await _candidateService.AddSkill(request);

            return Ok(result);
        }

        [HttpGet("skills/{userId}")]
        public async Task<IActionResult> GetSkills(int userId)
        {
            var result = await _candidateService.GetSkills(userId);

            return Ok(result);
        }

        [HttpDelete("skills/{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();
            int userId = int.Parse(userIdClaim);
            await _candidateService.DeleteSkill(id,userId);

            return Ok();
        }
        [HttpPost("education")]
        public async Task<IActionResult> AddEducation(
        CandidateEducationRequest request)
        {
            var result = await _candidateService.AddEducation(request);

            return Ok(result);
        }

        [HttpGet("education/{userId}")]
        public async Task<IActionResult> GetEducation(int userId)
        {
            var result = await _candidateService.GetEducation(userId);

            return Ok(result);
        }

        [HttpDelete("education/{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();
            int userId = int.Parse(userIdClaim);
            await _candidateService.DeleteEducation(id,userId);

            return Ok();
        }

        [HttpPost("experience")]
        public async Task<IActionResult> AddExperience(
        CandidateExperienceRequest request)
        {
            var result = await _candidateService.AddExperience(request);

            return Ok(result);
        }
        [HttpGet("experience/{userId}")]
        public async Task<IActionResult> GetExperience(int userId)
        {
            var result = await _candidateService.GetExperience(userId);

            return Ok(result);
        }

        [HttpDelete("experience/{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();
            int userId = int.Parse(userIdClaim);
            await _candidateService.DeleteExperience(id,userId);

            return Ok();
        }

        [HttpPost("/api/candidate/profile")]
        public async Task<IActionResult> GetFullProfile()
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();
            int userId = int.Parse(userIdClaim);
            var response = await _candidateService.GetFullProfile(userId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }
    }
}
