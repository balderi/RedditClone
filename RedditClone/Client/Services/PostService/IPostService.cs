namespace RedditClone.Client.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<Post>> GetPostByHashIdAsync(string hashId);
        Task<ServiceResponse<List<Post>>> GetPostsAsync();
        Task<ServiceResponse<Post>> AddPostAsync(PostNew post);
        Task<ServiceResponse<Post>> EditPostAsync(int commentId, string token, string newContent);
        Task<ServiceResponse<List<Post>>> GetPostsByAuthorAsync(Guid guid);
        Task<ServiceResponse<List<Post>>> GetPostsByBoardGuidAsync(Guid guid);
        Task<ServiceResponse<List<Post>>> GetPostsByBoardNameAsync(string name);
        Task<ServiceResponse<SubmissionVote>> UpVotePostAsync(VoteDTO vote);
        Task<ServiceResponse<SubmissionVote>> DownVotePostAsync(VoteDTO vote);
        Task<ServiceResponse<bool>> UnVotePostAsync(VoteDTO vote);
        Task<ServiceResponse<SubmissionVote>> GetPostVoteAsync(VoteDTO vote);
    }
}
