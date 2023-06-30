using RedditClone.Shared.Models;
using System.Net.Http.Json;

namespace RedditClone.Administrator.Services.BoardService
{
    public class BoardService : IBoardService
    {
        private readonly HttpClient _http;

        public BoardService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<Board>> AddBoardAsync(BoardNew board)
        {
            var result = await _http.PostAsJsonAsync($"{Settings.BaseUrl}/api/board", board);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Board>>();
        }

        public async Task<ServiceResponse<Board>> GetBoardByGuidAsync(Guid guid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Board>>($"{Settings.BaseUrl}/api/board/get/guid/{guid}");
            return result;
        }

        public async Task<ServiceResponse<Board>> GetBoardByNameAsync(string boardName)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Board>>($"{Settings.BaseUrl}/api/board/get/{boardName}");
            return result;
        }

        public async Task<ServiceResponse<List<Board>>> GetBoardsAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Board>>>($"{Settings.BaseUrl}/api/board/get");
            return result;
        }
    }
}
