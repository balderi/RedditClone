using RedditClone.Shared.Enums;

namespace RedditClone.Server.Services.VoteService
{
    public class VoteService : IVoteService
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public VoteService(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ServiceResponse<SubmissionVote>> AddSubmissionVoteAsync(VoteDTO voteDTO, VoteType voteType, VoteKind voteKind)
        {
            User user = new();
            Post? post;
            Comment? comment;

            try
            {
                user = await GetUserAsync(voteDTO.Token);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<SubmissionVote> { Message = ex.Message, Success = false };
            }

            if(voteKind == VoteKind.Post)
            {
                post = await _context.Posts.Where(p => p.HashId == voteDTO.Hash).FirstOrDefaultAsync();

                if(post == null) 
                {
                    return new ServiceResponse<SubmissionVote> { Success = false, Message = "Post not found." };
                }

                if(voteType == VoteType.Up)
                {
                    post.VotesUp += 1;
                }
                else
                {
                    post.VotesDown += 1;
                }
            }

            if(voteKind == VoteKind.Comment)
            {
                comment = await _context.Comments.Where(c => c.HashId == voteDTO.Hash).FirstOrDefaultAsync();

                if(comment == null)
                {
                    return new ServiceResponse<SubmissionVote> { Success = false, Message = "Comment not found." };
                }

                if(voteType == VoteType.Up)
                {
                    comment.VotesUp += 1;
                }
                else
                {
                    comment.VotesDown += 1;
                }
            }

            var vote = new SubmissionVote { SubmissionHash = voteDTO.Hash, UserGuid = user.Guid, VoteType = voteType };
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();

            return new ServiceResponse<SubmissionVote> { Data = vote };
        }

        public async Task<ServiceResponse<SubmissionVote>> EditSubmissionVoteAsync(VoteDTO voteDTO, VoteType voteType, VoteKind voteKind)
        {
            User user = new();
            Post? post;
            Comment? comment;

            try
            {
                user = await GetUserAsync(voteDTO.Token);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<SubmissionVote> { Message = ex.Message, Success = false };
            }

            if (voteKind == VoteKind.Post)
            {
                post = await _context.Posts.Where(p => p.HashId == voteDTO.Hash).FirstOrDefaultAsync();

                if (post == null)
                {
                    return new ServiceResponse<SubmissionVote> { Success = false, Message = "Post not found." };
                }

                if (voteType == VoteType.Up)
                {
                    post.VotesUp += 1;
                    post.VotesDown -= 1;
                }
                else
                {
                    post.VotesDown += 1;
                    post.VotesUp -= 1;
                }
            }

            if (voteKind == VoteKind.Comment)
            {
                comment = await _context.Comments.Where(c => c.HashId == voteDTO.Hash).FirstOrDefaultAsync();

                if (comment == null)
                {
                    return new ServiceResponse<SubmissionVote> { Success = false, Message = "Comment not found." };
                }

                if (voteType == VoteType.Up)
                {
                    comment.VotesUp += 1;
                    comment.VotesDown -= 1;
                }
                else
                {
                    comment.VotesDown += 1;
                    comment.VotesUp -= 1;
                }
            }

            var vote = await _context.Votes.Where(v => v.UserGuid == user.Guid && v.SubmissionHash == voteDTO.Hash).FirstOrDefaultAsync();

            if(vote == null)
            {
                return new ServiceResponse<SubmissionVote> { Success = false, Message = "Vote not found." };
            }

            vote.DateVoted = DateTime.UtcNow;
            vote.VoteType = voteType;

            await _context.SaveChangesAsync();

            return new ServiceResponse<SubmissionVote> {  Data = vote };
        }

        public async Task<ServiceResponse<SubmissionVote>> GetSubmissionVoteAsync(VoteDTO voteDTO, VoteKind voteKind)
        {
            User user = new();

            try
            {
                user = await GetUserAsync(voteDTO.Token);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<SubmissionVote> { Message = ex.Message, Success = false };
            }

            var vote = await _context.Votes.Where(v => v.UserGuid == user.Guid && v.SubmissionHash == voteDTO.Hash).FirstOrDefaultAsync();

            if (vote == null)
            {
                return new ServiceResponse<SubmissionVote> { Success = false, Message = "Vote not found." };
            }

            return new ServiceResponse<SubmissionVote> { Data = vote };
        }

        public async Task<ServiceResponse<bool>> RemoveSubmissionVoteAsync(VoteDTO voteDTO, VoteKind voteKind)
        {
            User user = new();
            Post? post;
            Comment? comment;

            try
            {
                user = await GetUserAsync(voteDTO.Token);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool> { Message = ex.Message, Success = false };
            }

            var vote = await _context.Votes.Where(v => v.UserGuid == user.Guid && v.SubmissionHash == voteDTO.Hash).FirstOrDefaultAsync();

            if (vote == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Vote not found." };
            }

            if (voteKind == VoteKind.Post)
            {
                post = await _context.Posts.Where(p => p.HashId == voteDTO.Hash).FirstOrDefaultAsync();

                if (post == null)
                {
                    return new ServiceResponse<bool> { Success = false, Message = "Post not found." };
                }

                if (vote.VoteType == VoteType.Up)
                {
                    post.VotesUp -= 1;
                }
                else
                {
                    post.VotesDown -= 1;
                }
            }

            if (voteKind == VoteKind.Comment)
            {
                comment = await _context.Comments.Where(c => c.HashId == voteDTO.Hash).FirstOrDefaultAsync();

                if (comment == null)
                {
                    return new ServiceResponse<bool> { Success = false, Message = "Comment not found." };
                }

                if (vote.VoteType == VoteType.Up)
                {
                    comment.VotesUp -= 1;
                }
                else
                {
                    comment.VotesDown -= 1;
                }
            }

            _context.Votes.Remove(vote);

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public Task<ServiceResponse<SubmissionVote>> VotePostAsync(string token, string postHash, VoteType voteType)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<SubmissionVote>> VoteCommentAsync(string token, string postHash, VoteType voteType)
        {
            throw new NotImplementedException();
        }

        async Task<User> GetUserAsync(string token)
        {
            var getUser = await _userService.GetUserByTokenAsync(token);
            if (getUser == null || !getUser.Success || getUser.Data == null)
            {
                throw new ArgumentException("User not found.", "token");
            }

            return getUser.Data;
        }
    }
}
