using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;

namespace JobPortalAPI.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IRecruiterService _recruiterService;
        private readonly ILogger<JobService> _logger;

        public JobService(
            IJobRepository jobRepository,
            IRecruiterService recruiterService,
            ILogger<JobService> logger)
        {
            _jobRepository = jobRepository;
            _recruiterService = recruiterService;
            _logger = logger;
        }


        // CREATE JOB
        public async Task<long> CreateJob(
            int userId,
            JobRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(
                        nameof(request));

                ValidateJobRequest(request);


                // Get recruiter
                var recruiter =
                    await _recruiterService.GetProfile(
                        userId);

                if (recruiter == null)
                    throw new UnauthorizedAccessException(
                        "Recruiter not found");


                // Get company
                if (recruiter.CompanyId == null ||
                    recruiter.CompanyId <= 0)
                {
                    throw new ArgumentException(
                        "Recruiter is not associated with a company");
                }


                // Create Job entity
                var job = new Job
                {
                    CompanyId =
                        recruiter.CompanyId.Value,

                    Title =
                        request.Title.Trim(),

                    Description =
                        request.Description.Trim(),

                    Location =
                        request.Location?.Trim(),

                    RefEmploymentType =
                        request.RefEmploymentType,

                    ExperienceLevel =
                        request.ExperienceLevel,

                    MinSalary =
                        request.MinSalary,

                    MaxSalary =
                        request.MaxSalary,

                    Deadline =
                        request.Deadline,

                    CreatedBy =
                        userId
                };


                return await _jobRepository.CreateJob(job);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating job");

                throw;
            }
        }


        // GET SINGLE JOB
        public async Task<JobResponse?> GetJob(
            long id)
        {
            if (id <= 0)
                return null;

            return await _jobRepository.GetJob(id);
        }


        // GET ALL JOBS
        public async Task<IEnumerable<JobResponse>> GetAllJobs()
        {
            return await _jobRepository.GetAllJobs();
        }


        // GET RECRUITER JOBS
        public async Task<IEnumerable<JobResponse>> GetRecruiterJobs(
            int userId)
        {
            var recruiter =
                await _recruiterService.GetProfile(
                    userId);

            if (recruiter == null)
                throw new UnauthorizedAccessException(
                    "Recruiter not found");


            if (recruiter.CompanyId == null ||
                recruiter.CompanyId <= 0)
            {
                throw new ArgumentException(
                    "Recruiter is not associated with a company");
            }


            return await _jobRepository.GetRecruiterJobs(
                recruiter.CompanyId.Value);
        }


        // UPDATE JOB
        public async Task<JobResponse> UpdateJob(
            long id,
            int userId,
            JobRequest request)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException(
                        "Invalid job id");

                if (request == null)
                    throw new ArgumentNullException(
                        nameof(request));

                ValidateJobRequest(request);


                // Get recruiter
                var recruiter =
                    await _recruiterService.GetProfile(
                        userId);

                if (recruiter == null)
                    throw new UnauthorizedAccessException(
                        "Recruiter not found");


                // Get company
                if (recruiter.CompanyId == null ||
                    recruiter.CompanyId <= 0)
                {
                    throw new ArgumentException(
                        "Recruiter is not associated with a company");
                }


                // Create Job entity
                var job = new Job
                {
                    Id =
                        id,

                    CompanyId =
                        recruiter.CompanyId.Value,

                    Title =
                        request.Title.Trim(),

                    Description =
                        request.Description.Trim(),

                    Location =
                        request.Location?.Trim(),

                    RefEmploymentType =
                        request.RefEmploymentType,

                    ExperienceLevel =
                        request.ExperienceLevel,

                    MinSalary =
                        request.MinSalary,

                    MaxSalary =
                        request.MaxSalary,

                    Deadline =
                        request.Deadline,

                    UpdatedBy =
                        userId
                };


                return await _jobRepository.UpdateJob(
                    job);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating job");

                throw;
            }
        }


        // DELETE JOB
        public async Task<bool> DeleteJob(
            long id,
            int userId)
        {
            try
            {
                if (id <= 0)
                    return false;


                // Get recruiter
                var recruiter =
                    await _recruiterService.GetProfile(
                        userId);

                if (recruiter == null)
                    throw new UnauthorizedAccessException(
                        "Recruiter not found");


                // Get company
                if (recruiter.CompanyId == null ||
                    recruiter.CompanyId <= 0)
                {
                    throw new ArgumentException(
                        "Recruiter is not associated with a company");
                }


                return await _jobRepository.DeleteJob(
                    id,
                    recruiter.CompanyId.Value,
                    userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting job");

                throw;
            }
        }


        // COMMON VALIDATION
        private void ValidateJobRequest(
            JobRequest request)
        {
            if (string.IsNullOrWhiteSpace(
                request.Title))
            {
                throw new ArgumentException(
                    "Job title is required");
            }


            if (string.IsNullOrWhiteSpace(
                request.Description))
            {
                throw new ArgumentException(
                    "Job description is required");
            }


            if (request.MinSalary.HasValue &&
                request.MaxSalary.HasValue &&
                request.MinSalary >
                request.MaxSalary)
            {
                throw new ArgumentException(
                    "Minimum salary cannot be greater than maximum salary");
            }
        }
    }
}