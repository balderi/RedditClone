using System.Xml.Linq;

namespace RedditClone.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<User>> GetUserByEmailAsync(string email)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<User>>($"api/user/get/email/{email}");
            return result;
        }

        public async Task<ServiceResponse<User>> GetUserByGuidAsync(Guid guid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<User>>($"api/user/get/guid/{guid}");
            return result;
        }

        public async Task<ServiceResponse<User>> GetUserByIdAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<User>>($"api/user/get/id/{id}");
            return result;
        }

        public async Task<ServiceResponse<User>> GetUserByNameAsync(string name)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<User>>($"api/user/get/name/{name}");
            return result;
        }

        public async Task<ServiceResponse<User>> GetUserByTokenAsync(string token)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<User>>($"api/user/get/token/{token}");
            return result;
        }
    }
}
