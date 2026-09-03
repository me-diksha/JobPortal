using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Services;
using JobPortalAPI.Services.Abstractions;
using JobPortalAPI.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPortalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IRecruiterService _recruiterService;
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
        public JobController(IJobService jobService,IRecruiterService recruiterService)
        {
            _jobService = jobService;
            _recruiterService = recruiterService;
        }

        [HttpGet("createJob")]
        public async Task<IActionResult> CreateJob(JobRequest request)
        {
            var recruiter = await _recruiterService.GetProfile(UserId);

            if (recruiter == null)
                return Unauthorized();

            if (recruiter.CompanyId == null)
                return BadRequest("Recruiter is not associated with a company");

            var job = new Job
            {
                CompanyId = recruiter.CompanyId.Value,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                RefEmploymentType = request.RefEmploymentType,
                ExperienceLevel = request.ExperienceLevel,
                MinSalary = request.MinSalary,
                MaxSalary = request.MaxSalary,
                Deadline = request.Deadline,
                CreatedBy = UserId
            };
            var result =await _jobService.CreateJob(job);


            return Ok(new
            {
                message = "Job created successfully",
                id = result
            });
        }

    }
}
