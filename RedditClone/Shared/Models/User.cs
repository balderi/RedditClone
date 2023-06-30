namespace RedditClone.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public User GetDTO() => new User { Id = Id, Guid = Guid, Username = Username, DateCreated = DateCreated };
    }
}
