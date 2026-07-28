using JobPortalAPI.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Services.Abstractions;
using JobPortalAPI.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPortalAPI.Controllers
{
    [Authorize(Policy = PolicyNames.RecruiterOnly)]
    [ApiController]
    [Route("api/recruiter")]
    public class RecruiterController : ControllerBase
    {
       
        private IRecruiterService _recruiterService;
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
        public RecruiterController( IRecruiterService recruiterService)
        {
          
            _recruiterService = recruiterService;
        }

        [HttpGet("getrecruiterprofile")]
        public async Task<IActionResult> GetFullProfile()
        {

            var response = await _recruiterService.GetProfile(UserId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost("createprofile")]
        public async Task<IActionResult> CreateProfile(
        RecruiterProfileRequest request)
        {

            var response = await _recruiterService.CreateProfile(request, UserId);
            if (response == 0)
                return Ok("Profile Not Created");
            return Ok(response);
        }

        [HttpPut("updateprofile")]
        public async Task<IActionResult>UpdateProfile(RecruiterProfileRequest request)
        {
            var response = await _recruiterService.UpdateProfile(request, UserId);
            if (response == 0)
                return Ok("Profile couldn't be updated. Please try later !");
            return Ok(response);
        }

        [HttpDelete("deleteprofile")]
        public async Task<IActionResult> DeleteProfile(int userId)
        {
            var result = await _recruiterService.DeleteProfile(userId);

            if (!result)
                return NotFound(new
                {
                    message = "Recruiter profile not found"
                });

            return Ok("Recruiter profile deleted successfully.");
        }
    }
}
