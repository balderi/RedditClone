namespace RedditClone.Shared.Models
{
    public class PostEdit
    {
        public int PostId { get; set; }
        public string UserToken { get; set; } = string.Empty;
        public string NewContent { get; set; } = string.Empty;
    }
}
