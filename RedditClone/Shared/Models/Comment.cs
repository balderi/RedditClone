using System.ComponentModel.DataAnnotations.Schema;

namespace RedditClone.Shared.Models
{
    public class Comment : ISubmission
    {
        public int Id { get; set; }
        public string HashId { get; set; } = string.Empty;  
        public int AuthorId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int ParentId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public bool Hidden { get; set; }
        public bool Deleted { get; set; }
        public bool Edited { get; set; }
        public DateTime DateEdited { get; set; }
        public int VotesUp { get; set; }
        public int VotesDown { get; set; }
        public int Points => VotesUp - VotesDown;
    }
}
