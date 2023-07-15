namespace RedditClone.Shared.Models
{
    public interface ISubmission
    {
        public int Id { get; set; }
        public string HashId { get; set; }
        public int AuthorId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateEdited { get; set; }
        public int VotesUp { get; set; }
        public int VotesDown { get; set; }
        public bool Hidden { get; set; }
        public bool Deleted { get; set; }
        public bool Edited { get; set; }
    }
}
