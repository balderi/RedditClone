using System.ComponentModel.DataAnnotations;

namespace RedditClone.Shared.Models
{
    public class BoardNew
    {
        [Required]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "Must be between 3 and 35 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(255, ErrorMessage = "Must be less than 256 characters.")]
        public string Description { get; set; } = string.Empty;
        public User Owner { get; set; }
        public bool Nsfw { get; set; }
    }
}
