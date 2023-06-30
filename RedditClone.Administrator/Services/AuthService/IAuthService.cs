using RedditClone.Shared.Models;

namespace RedditClone.Administrator.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> RegisterAsync(UserRegister request);
        Task<ServiceResponse<string>> LoginAsync(UserLogin request);
        Task<ServiceResponse<bool>> ChangePasswordAsync(UserChangePassword request);
    }
}
