using AutoMapper;
using JobPortalAPI.Common;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;
using System.ComponentModel.Design;

namespace JobPortalAPI.Services.Implementations
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IRecruiterRepository _recruiterRepository;
        private readonly ILogger<RecruiterService> _logger;
        private readonly IMapper _mapper;


        public RecruiterService(
            IRecruiterRepository recruiterRepository,
            ILogger<RecruiterService> logger, IMapper mappingProfile)
        {
            _recruiterRepository = recruiterRepository;
            _logger = logger;
            _mapper = mappingProfile;
        }

        public async Task<long> CreateProfile(RecruiterProfileRequest request,int userid)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));


                if (userid <= 0)
                    throw new ArgumentException("Invalid user id");


                if (string.IsNullOrWhiteSpace(request.FirstName))
                    throw new ArgumentException("First name is required");


                if (string.IsNullOrWhiteSpace(request.LastName))
                    throw new ArgumentException("Last name is required");

                var recruiter = new Recruiter
                {
                    UserId = userid,
                    CompanyId = request.CompanyId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Designation = request.Designation,
                    Department = request.Department,
                    Phone = request.Phone,
                    LinkedinUrl = request.LinkedinUrl
                };
                return await _recruiterRepository.CreateProfile(recruiter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in creating recruiter profile");
                throw;
            }
        }



        public async Task<RecruiterProfileResponse> GetProfile(int userId)
        {
            try
            {
                if (userId <= 0)
                    return null;


                var response= _recruiterRepository.GetProfile(userId);
                return _mapper.Map<RecruiterProfileResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting recruiter profile");
                throw;
            }
        }



        public async Task<long> UpdateProfile(RecruiterProfileRequest request,int userid)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));
                if(userid <= 0)
                    throw new ArgumentException("Invalid user id");
                if (string.IsNullOrWhiteSpace(request.FirstName))
                    throw new ArgumentException("First name is required.");

                if (string.IsNullOrWhiteSpace(request.LastName))
                    throw new ArgumentException("Last name is required.");
                var recruiter = new Recruiter
                {
                    UserId = userid,
                    CompanyId = request.CompanyId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Designation = request.Designation,
                    Department = request.Department,
                    Phone = request.Phone,
                    LinkedinUrl = request.LinkedinUrl
                };
                return await _recruiterRepository.UpdateProfile(recruiter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in updating recruiter profile");
                throw;
            }
        }



        public async Task<bool> DeleteProfile(int userId)
        {
            try
            {
                if (userId <= 0)
                    return false;


                return await _recruiterRepository.DeleteProfile(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in deleting recruiter profile");
                throw;
            }
        }
    }

}
