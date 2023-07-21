using System.ComponentModel.DataAnnotations.Schema;

namespace RedditClone.Shared.Models
{
    public class Post : ISubmission
    {
        public int Id { get; set; }
        public string HashId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public bool Hidden { get; set; }
        public bool Deleted { get; set; }
        public bool Edited { get; set; }
        public bool Spoiler { get; set; }
        public DateTime DateEdited { get; set; }
        public int VotesUp { get; set; }
        public int VotesDown { get; set; }

        public int Points => VotesUp - VotesDown;

        public override string ToString()
        {
            return Title;
        }
    }
}
