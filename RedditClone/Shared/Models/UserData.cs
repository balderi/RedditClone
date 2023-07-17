namespace RedditClone.Shared.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
