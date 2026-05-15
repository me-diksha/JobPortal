using JobPortalAPI.Models.Responses;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity.Data;
using JobPortalAPI.Models.Requests;

namespace JobPortalAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse?> Register(Models.Requests.RegisterRequest request);
        Task<AuthResponse?> Login(Models.Requests.LoginRequest request);
    }
}
