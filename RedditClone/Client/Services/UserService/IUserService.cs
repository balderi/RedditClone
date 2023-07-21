namespace RedditClone.Client.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetUsersAsync();
        Task<ServiceResponse<User>> GetUserByIdAsync(int id);
        Task<ServiceResponse<User>> GetUserByGuidAsync(Guid guid);
        Task<ServiceResponse<User>> GetUserByEmailAsync(string email);
        Task<ServiceResponse<User>> GetUserByNameAsync(string name);
        Task<ServiceResponse<User>> GetUserByTokenAsync(string token);
    }
}
