using System.ComponentModel.DataAnnotations;

namespace RedditClone.Shared.Models
{
    public class UserRegister
    {
        [Required, StringLength(20), MinLength(1)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(100), MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
