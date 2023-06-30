using RedditClone.Shared.Models;
using System.Net.Http.Json;

namespace RedditClone.Administrator.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private string _url;

        public AuthService(HttpClient http, string url)
        {
            _http = http;
            _url = url;
        }

        public async Task<ServiceResponse<bool>> ChangePasswordAsync(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync($"{_url}/api/auth/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<string>> LoginAsync(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync($"{_url}/api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> RegisterAsync(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync($"{_url}/api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
