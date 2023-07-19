using RedditClone.Shared.Enums;

namespace RedditClone.Shared.Models
{
    public class VoteDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public bool Existing { get; set; }
    }
}
