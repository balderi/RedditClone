using System.Text.RegularExpressions;

namespace RedditClone.Server.Services.BoardService
{
    public class BoardService : IBoardService
    {
        private readonly DataContext _context;

        public BoardService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Board>> AddBoardAsync(BoardNew board)
        {
            var response = new ServiceResponse<Board>();
            Regex regex = new Regex("^([a-zA-Z0-9_])+$");
            if (!regex.IsMatch(board.Name))
            {
                response.Success = false;
                response.Message = $"Board name contains one or more illegal characters.";
                return response;
            }
            var user = await _context.Users.FindAsync(board.Owner.Id);
            if (user == null)
            {
                response.Success = false;
                response.Message = $"User not found.";
                return response;
            }
            Board newBoard = new Board { Name = board.Name, Description = board.Description, Owner = user, Nsfw = board.Nsfw };
            await _context.Boards.AddAsync(newBoard);
            await _context.SaveChangesAsync();
            response.Data = newBoard;
            return response;
        }

        public async Task<ServiceResponse<Board>> GetBoardByGuidAsync(Guid guid)
        {
            var response = new ServiceResponse<Board>();
            var board = await _context.Boards.Where(b => b.Guid == guid).Include(b => b.Owner).FirstOrDefaultAsync();
            if (board == null)
            {
                response.Success = false;
                response.Message = $"Board with ID '{guid}' was not found.";
            }
            response.Data = board;
            return response;
        }

        public async Task<ServiceResponse<Board>> GetBoardByNameAsync(string boardName)
        {
            var response = new ServiceResponse<Board>();
            var board = await _context.Boards.Where(b => b.Name == boardName).Include(b => b.Owner).FirstOrDefaultAsync();
            if(board == null)
            {
                response.Success = false;
                response.Message = $"The board '{boardName}' was not found.";
            }
            response.Data = board;
            return response;
        }

        public async Task<ServiceResponse<List<Board>>> GetBoardsAsync()
        {
            var response = new ServiceResponse<List<Board>>();
            var boards = await _context.Boards.Include(b => b.Owner).ToListAsync();
            if (boards == null || !boards.Any())
            {
                response.Success = false;
                response.Message = $"No boards found.";
            }
            response.Data = boards;
            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetBoardSearchSuggestionsAsync(string queryText)
        {
            var retval = new List<string>();

            if (string.IsNullOrEmpty(queryText))
            {
                return new ServiceResponse<List<string>> { Success = false, Message = "Empty query" };
            }

            var start = await _context.Boards.Where(b => b.Name.ToLower().StartsWith(queryText)).OrderBy(b => b.Name).ToListAsync();
            foreach(var item in start)
            {
                retval.Add(item.Name);
            }

            var boards = await _context.Boards.Where(b => 
                b.Name.ToLower().Contains(queryText.ToLower()) || 
                b.Description.ToLower().Contains(queryText.ToLower())
            ).OrderBy(b => b.Name).ToListAsync();

            foreach(var board in boards)
            {
                if(!retval.Contains(board.Name))
                {
                    retval.Add(board.Name);
                }
            }

            return new ServiceResponse<List<string>> { Data = retval };
        }
    }
}
