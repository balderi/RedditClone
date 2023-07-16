using RedditClone.Server.Services.UserService;
using RedditClone.Shared;
using RedditClone.Shared.Models;
using System;

namespace RedditClone.Server.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IBoardService _boardService;

        public PostService(DataContext context, IUserService userService, IBoardService boardService)
        {
            _context = context;
            _userService = userService;
            _boardService = boardService;
        }

        public async Task<ServiceResponse<Post>> AddPostAsync(PostNew post)
        {
            ServiceResponse<Post> response = new();

            var user = await _userService.GetUserByGuidAsync(post.AuthorGuid);
            if (user == null || !user.Success || user.Data == null)
            {
                response.Success = false;
                response.Message = "That user does not exist";
                return response;
            }
            var board = await _boardService.GetBoardByGuidAsync(post.BoardGuid);
            if (board == null || !board.Success || board.Data == null)
            {
                response.Success = false;
                response.Message = "That user does not exist";
                return response;
            }

            Post newPost = new Post { Title = post.Title, Content = post.Content, Link = post.Link, AuthorId = user.Data.Id, Board = board.Data };
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            newPost.HashId = Hashing.Encode(newPost.Id);
            await _context.SaveChangesAsync();

            response.Data = newPost;
            return response;
        }

        public async Task<ServiceResponse<Post>> GetPostByHashIdAsync(string hashId)
        {
            ServiceResponse<Post> response = new();
            var post = await _context.Posts.Include(p => p.Board).ThenInclude(b => b.Owner).Where(p => p.HashId == hashId).FirstOrDefaultAsync();
            if (post == null)
            {
                response.Success = false;
                response.Message = $"Post with ID '{hashId}' not found.";
                return response;
            }

            response.Data = post;
            return response;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsByAuthorAsync(Guid guid)
        {
            ServiceResponse<List<Post>> response = new();
            var user = await _context.Users.Where(u => u.Guid == guid).FirstOrDefaultAsync();
            if (user == null) 
            { 
                response.Success = false;
                response.Message = $"User with GUID '{guid}' not found.";
                return response;
            }
            var posts = await _context.Posts.Include(p => p.Board).Where(p => p.AuthorId == user.Id).ToListAsync();
            response.Data = posts;
            return response;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsAsync()
        {
            ServiceResponse<List<Post>> response = new();
            var posts = await _context.Posts.Include(p => p.Board).ThenInclude(b => b.Owner).ToListAsync();
            if(posts == null || !posts.Any())
            {
                response.Success = false;
                response.Message = "No posts found.";
                return response;
            }

            response.Data = posts;
            return response;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsByBoardGuidAsync(Guid guid)
        {
            ServiceResponse<List<Post>> response = new();
            var board = await _boardService.GetBoardByGuidAsync(guid);
            if (board == null || !board.Success || board.Data == null)
            {
                response.Success = false;
                response.Message = $"Board with ID '{guid}' does not exist";
                return response;
            }
            var post = await _context.Posts.Include(p => p.Board).ThenInclude(b => b.Owner).Where(p => p.Board.Id == board.Data.Id).ToListAsync();
            if (post == null)
            {
                response.Success = false;
                response.Message = $"Post with board ID '{guid}' not found.";
                return response;
            }

            response.Data = post;
            return response;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostsByBoardNameAsync(string name)
        {
            ServiceResponse<List<Post>> response = new();
            var board = await _boardService.GetBoardByNameAsync(name);
            if (board == null || !board.Success || board.Data == null)
            {
                response.Success = false;
                response.Message = $"The board '{name}' does not exist";
                return response;
            }
            var post = await _context.Posts.Include(p => p.Board).ThenInclude(b => b.Owner).Where(p => p.Board.Id == board.Data.Id).ToListAsync();
            if (post == null)
            {
                response.Success = false;
                response.Message = $"Post with board name '{name}' not found.";
                return response;
            }

            response.Data = post;
            return response;
        }

        public async Task<ServiceResponse<Post>> EditPostAsync(PostEdit edit)
        {
            ServiceResponse<Post> response = new();

            var user = await _userService.GetUserByTokenAsync(edit.UserToken);
            if (user == null || !user.Success || user.Data == null)
            {
                response.Success = false;
                response.Message = "That user does not exist";
                return response;
            }

            var post = await _context.Posts.FindAsync(edit.PostId);
            if (post == null)
            {
                response.Success = false;
                response.Message = "That post does not exist";
                return response;
            }

            post.Content = edit.NewContent;
            post.Edited = true;
            post.DateEdited = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            response.Data = post;
            return response;
        }
    }
}
