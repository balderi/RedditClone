using RedditClone.Shared.Enums;

namespace RedditClone.Server.Services.VoteService
{
    public interface IVoteService
    {
        Task<ServiceResponse<SubmissionVote>> AddSubmissionVoteAsync(VoteDTO voteDTO, VoteType voteType, VoteKind voteKind);
        Task<ServiceResponse<SubmissionVote>> GetSubmissionVoteAsync(VoteDTO voteDTO, VoteKind voteKind);
        Task<ServiceResponse<SubmissionVote>> EditSubmissionVoteAsync(VoteDTO voteDTO, VoteType voteType, VoteKind voteKind);
        Task<ServiceResponse<bool>> RemoveSubmissionVoteAsync(VoteDTO voteDTO, VoteKind voteKind);
        Task<ServiceResponse<SubmissionVote>> VotePostAsync(string token, string postHash, VoteType voteType);
        Task<ServiceResponse<SubmissionVote>> VoteCommentAsync(string token, string postHash, VoteType voteType);
    }
}
