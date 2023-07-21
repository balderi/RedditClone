using System.ComponentModel.DataAnnotations.Schema;

namespace RedditClone.Shared.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid Guid { get; set; } = Guid.NewGuid();
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        public bool Nsfw { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return Name;
        }
    }
}
