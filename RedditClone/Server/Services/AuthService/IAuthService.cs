﻿namespace RedditClone.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> RegisterAsync(UserRegister userRegister);
        Task<bool> UserExists(UserRegister user);
        Task<ServiceResponse<string>> LoginAsync(string email, string password);
        Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    }
}
