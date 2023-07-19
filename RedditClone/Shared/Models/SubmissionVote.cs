using RedditClone.Shared.Enums;

namespace RedditClone.Shared.Models
{
    public class SubmissionVote
    {
        public int Id { get; set; }
        public Guid UserGuid { get; set; }
        public string SubmissionHash { get; set; } = string.Empty;
        public DateTime DateVoted { get; set; } = DateTime.UtcNow;
        public VoteType VoteType { get; set; }
    }
}
