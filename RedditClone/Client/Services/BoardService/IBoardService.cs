namespace RedditClone.Client.Services.BoardService
{
    public interface IBoardService
    {
        Task<ServiceResponse<Board>> GetBoardByNameAsync(string boardName);
        Task<ServiceResponse<Board>> GetBoardByGuidAsync(Guid guid);
        Task<ServiceResponse<List<Board>>> GetBoardsAsync();
        Task<ServiceResponse<Board>> AddBoardAsync(BoardNew board);
    }
}
