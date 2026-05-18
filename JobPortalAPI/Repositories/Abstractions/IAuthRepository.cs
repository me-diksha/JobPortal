using JobPortalAPI.Models.Common;

namespace JobPortalAPI.Repositories.Abstractions
{
    public interface IAuthRepository
    {
       Task<User?> GetUserByEmail(string email);
       Task<long> CreateUser(User user);
      


    }
}
