namespace RedditClone.Shared.Models
{
    public class CommentEdit
    {
        public int CommentId { get; set; }
        public string UserToken { get; set; } = string.Empty;
        public string NewContent { get; set; } = string.Empty;
    }
}
