namespace RedditClone.Server.Services.CommentService
{
    public interface ICommentService
    {
        Task<ServiceResponse<List<Comment>>> GetCommentsByParentHashAsync(string parentHash);
        Task<ServiceResponse<List<Comment>>> GetCommentsByPostHashAsync(string postHash);
        Task<ServiceResponse<int>> CountCommentsByPostHashAsync(string postHash);
        Task<ServiceResponse<Comment>> GetCommentByHashAsync(string hash);
        Task<ServiceResponse<Comment>> AddCommentAsync(CommentNew comment);
        Task<ServiceResponse<List<Comment>>> GetCommentsByAuthorAsync(Guid guid);
    }
}
