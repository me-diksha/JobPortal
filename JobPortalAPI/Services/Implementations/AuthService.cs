using BCrypt.Net;
using JobPortalAPI.Common;
using JobPortalAPI.DataAccess;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;
using Microsoft.Win32;

namespace JobPortalAPI.Services.Implementations
{
    public class AuthService :IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly IJwtService _jwtService;
        public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger, IJwtService jwtService)
        {
            _authRepository = authRepository;
            _logger = logger;
            this._jwtService = jwtService;
        }

        public async Task<AuthResponse?> Register(Models.Requests.RegisterRequest request) {

            var existingUser = await _authRepository.GetUserByEmail(request.Email);
            if (existingUser != null)
                return null;
            if (request.RoleId != (int)ActorType.Candidate &&
         request.RoleId != (int)ActorType.Recruiter)
            {
                return null;
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                RoleId = request.RoleId
            };
            var newUserId = await _authRepository.CreateUser(user);

            if (newUserId <= 0)
                return null;
            var roleName = GetRoleName(user.RoleId);
            var permissions = GetPermissions(user.RoleId);
            var token = _jwtService.GenerateAccessToken(
            newUserId,
            user.Email,
            user.Name,
            roleName,
            permissions
        );
            return new AuthResponse
            {
                Id = newUserId,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }
        public async Task<AuthResponse?> Login(Models.Requests.LoginRequest request) {
            var user = await _authRepository.GetUserByEmail(request.Email);

            if (user == null)
                return null;

            var validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );
            if (!validPassword)
                return null;

            
            var roleName = GetRoleName(user.RoleId);

       
            var permissions = GetPermissions(user.RoleId);
            var token = _jwtService.GenerateAccessToken(
           user.Id,
           user.Email,
           user.Name,
           roleName,
           permissions
       );

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }
       private string GetRoleName(int roleId)
        {
            return roleId switch
            {
                (int)ActorType.Candidate => "candidate",

                (int)ActorType.Recruiter => "recruiter",

                (int)ActorType.Admin => "admin",

                _ => throw new Exception("Invalid role")
            };
        }
        private List<string> GetPermissions(int roleId)
        {
            return roleId switch
            {
                (int)ActorType.Candidate => new List<string>
            {
                Permissions.ApplyJob,
                Permissions.EditProfile
            },

                (int)ActorType.Recruiter => new List<string>
            {
                Permissions.CreateJob,
                Permissions.EditJob,
                Permissions.ViewApplications
            },

                (int)ActorType.Admin => new List<string>
            {
                Permissions.ManageUsers
            },

                _ => new List<string>()
            };
        }
    }
}
