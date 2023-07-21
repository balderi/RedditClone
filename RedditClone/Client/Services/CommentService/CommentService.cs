using RedditClone.Shared.Models;

namespace RedditClone.Client.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _http;

        public CommentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<Comment>> AddCommentAsync(CommentNew comment)
        {
            var result = await _http.PostAsJsonAsync("api/comment", comment);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Comment>>();
        }

        public async Task<ServiceResponse<int>> CountCommentsByPostHashAsync(string postHash)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>($"api/comment/count/{postHash}");
            return result;
        }

        public async Task<ServiceResponse<Comment>> GetCommentByHashAsync(string hash)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Comment>>($"api/comment/get/{hash}");
            return result;
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentsAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Comment>>>($"api/comment");
            return result;
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentsByParentHashAsync(string parentHash)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Comment>>>($"api/comment/get/parent/{parentHash}");
            return result;
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentsByPostHashAsync(string postHash)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Comment>>>($"api/comment/get/post/{postHash}");
            return result;
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentsByAuthorAsync(Guid guid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Comment>>>($"api/comment/get/author/{guid}");
            return result;
        }

        public async Task<ServiceResponse<Comment>> EditCommentAsync(int commentId, string token, string newContent)
        {
            var edit = new CommentEdit { CommentId = commentId, UserToken = token, NewContent = newContent };
            var result = await _http.PutAsJsonAsync("api/comment", edit);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Comment>>();
        }

        public async Task<ServiceResponse<SubmissionVote>> GetCommentVoteAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/comment/vote/get", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<SubmissionVote>>();
        }

        public async Task<ServiceResponse<SubmissionVote>> UpVoteCommentAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/comment/vote/up", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<SubmissionVote>>();
        }

        public async Task<ServiceResponse<SubmissionVote>> DownVoteCommentAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/comment/vote/down", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<SubmissionVote>>();
        }

        public async Task<ServiceResponse<bool>> UnVoteCommentAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/comment/vote/remove", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
