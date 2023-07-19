namespace RedditClone.Client.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly HttpClient _http;

        public PostService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<Post>> AddPostAsync(PostNew post)
        {
            var result = await _http.PostAsJsonAsync("api/post", post);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Post>>();
        }

        public async Task<ServiceResponse<Post>> EditPostAsync(int postId, string token, string newContent)
        {
            var edit = new PostEdit { PostId = postId, UserToken = token, NewContent = newContent };

            var result = await _http.PutAsJsonAsync("api/post", edit);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Post>>();
        }

        public async Task<ServiceResponse<Post>> GetPostByHashIdAsync(string hashId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Post>>($"api/post/get/{hashId}");
            return result;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Post>>>("api/post/get/all");
            return result;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsByAuthorAsync(Guid guid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Post>>>($"api/post/get/author/{guid}");
            return result;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsByBoardGuidAsync(Guid guid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Post>>>($"api/post/get/board/guid/{guid}");
            return result;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsByBoardNameAsync(string name)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Post>>>($"api/post/get/board/{name}");
            return result;
        }

        public async Task<ServiceResponse<SubmissionVote>> GetPostVoteAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/post/vote/get", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<SubmissionVote>>();
        }

        public async Task<ServiceResponse<SubmissionVote>> UpVotePostAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/post/vote/up", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<SubmissionVote>>();
        }

        public async Task<ServiceResponse<SubmissionVote>> DownVotePostAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/post/vote/down", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<SubmissionVote>>();
        }

        public async Task<ServiceResponse<bool>> UnVotePostAsync(VoteDTO vote)
        {
            var result = await _http.PostAsJsonAsync("api/post/vote/remove", vote);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
