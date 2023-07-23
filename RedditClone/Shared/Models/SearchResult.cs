namespace RedditClone.Shared.Models
{
    public class SearchResult
    {
        public List<Post> Posts { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
