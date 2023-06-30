using System.ComponentModel.DataAnnotations;

namespace RedditClone.Shared.Models
{
    public class CommentNew
    {
        [Required]
        [StringLength(10000, ErrorMessage = "Comment must be less than 10000 characters.")]
        public string Content { get; set; } = string.Empty;
        public Guid AuthorGuid { get; set; }
        public string PostHash { get; set; } = string.Empty;
        public string ParentHash { get; set; } = string.Empty;
    }
}
