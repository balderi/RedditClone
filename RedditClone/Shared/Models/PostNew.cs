using System.ComponentModel.DataAnnotations;

namespace RedditClone.Shared.Models
{
    public class PostNew
    {
        [Required]
        [StringLength(300, ErrorMessage = "Title must be less than 100 characters.")]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string BoardName { get; set; } = string.Empty;
        public Guid AuthorGuid { get; set; }
        public Guid BoardGuid { get; set; }
    }
}
