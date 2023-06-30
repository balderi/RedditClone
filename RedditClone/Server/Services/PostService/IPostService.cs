namespace RedditClone.Server.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<Post>> GetPostByHashIdAsync(string hashId);
        Task<ServiceResponse<List<Post>>> GetPostsAsync();
        Task<ServiceResponse<Post>> AddPostAsync(PostNew post);
        Task<ServiceResponse<List<Post>>> GetPostsByAuthorAsync(Guid guid);
        Task<ServiceResponse<List<Post>>> GetPostsByBoardGuidAsync(Guid guid);
        Task<ServiceResponse<List<Post>>> GetPostsByBoardNameAsync(string name);
    }
}
