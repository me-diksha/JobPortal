using JobPortalAPI.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPortalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        private int UserId
        {
            get
            {
                var claim =
                    User.FindFirst(
                        ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(claim))
                    throw new UnauthorizedAccessException();

                return int.Parse(claim);
            }
        }

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }


        // CREATE JOB
        [HttpPost("createJob")]
        [Authorize(Policy = Permissions.CreateJob)]
        public async Task<IActionResult> CreateJob(
            [FromBody] JobRequest request)
        {
            var result =
                await _jobService.CreateJob(
                    UserId,
                    request);

            return Ok(new
            {
                message = "Job created successfully",
                id = result
            });
        }


        // GET SINGLE JOB
        [HttpGet("getjob")]
        public async Task<IActionResult> GetJob(long id)
        {
            var job =
                await _jobService.GetJob(id);

            if (job == null)
                return NotFound("Job not found");

            return Ok(job);
        }


        // GET ALL JOBS
        [HttpGet("getalljobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs =
                await _jobService.GetAllJobs();

            return Ok(jobs);
        }


        // GET RECRUITER JOBS
        [HttpGet("getrecruiterjobs")]
        public async Task<IActionResult> GetRecruiterJobs()
        {
            var jobs =
                await _jobService.GetRecruiterJobs(
                    UserId);

            return Ok(jobs);
        }


        // UPDATE JOB
        [HttpPut("updatejob")]
        [Authorize(Policy = Permissions.EditJob)]
        public async Task<IActionResult> UpdateJob(
            long id,
            [FromBody] JobRequest request)
        {
            var result =
                await _jobService.UpdateJob(
                    id,
                    UserId,
                    request);

            return Ok(new
            {
                message = "Job updated successfully",
                job = result
            });
        }


        // DELETE JOB
        [HttpDelete("deletejob")]
        [Authorize(Policy = Permissions.EditJob)]
        public async Task<IActionResult> DeleteJob(long id)
        {
            var result =
                await _jobService.DeleteJob(
                    id,
                    UserId);

            if (!result)
                return NotFound("Job not found");

            return Ok(new
            {
                message = "Job deleted successfully"
            });
        }
    }
}