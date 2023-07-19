namespace RedditClone.Client.Services.CommentService
{
    public interface ICommentService
    {
        Task<ServiceResponse<List<Comment>>> GetCommentsByParentHashAsync(string parentHash);
        Task<ServiceResponse<List<Comment>>> GetCommentsByPostHashAsync(string postHash);
        Task<ServiceResponse<int>> CountCommentsByPostHashAsync(string postHash);
        Task<ServiceResponse<Comment>> GetCommentByHashAsync(string hash);
        Task<ServiceResponse<Comment>> AddCommentAsync(CommentNew comment);
        Task<ServiceResponse<List<Comment>>> GetCommentsByAuthorAsync(Guid guid);
        Task<ServiceResponse<Comment>> EditCommentAsync(int commentId, string token, string newContent);
        Task<ServiceResponse<SubmissionVote>> GetCommentVoteAsync(VoteDTO vote);
        Task<ServiceResponse<SubmissionVote>> UpVoteCommentAsync(VoteDTO vote);
        Task<ServiceResponse<SubmissionVote>> DownVoteCommentAsync(VoteDTO vote);
        Task<ServiceResponse<bool>> UnVoteCommentAsync(VoteDTO vote);
    }
}
