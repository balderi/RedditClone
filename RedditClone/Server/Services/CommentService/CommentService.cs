using RedditClone.Shared;
using System;

namespace RedditClone.Server.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public CommentService(DataContext context, IPostService postService, IUserService userService)
        {
            _context = context;
            _postService = postService;
            _userService = userService;
        }

        public async Task<ServiceResponse<Comment>> AddCommentAsync(CommentNew comment)
        {
            var response = new ServiceResponse<Comment>();
            var post = await _postService.GetPostByHashIdAsync(comment.PostHash);
            if(post == null || !post.Success || post.Data == null)
            {
                response.Success = false;
                response.Message = "Post not found.";
                return response;
            }
            var user = await _userService.GetUserByGuidAsync(comment.AuthorGuid);
            if(user == null || !user.Success || user.Data == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }

            Comment newComment = new Comment { Content = comment.Content, AuthorId = user.Data.Id, Post = post.Data };

            if (!string.IsNullOrEmpty(comment.ParentHash))
            {
                var parent = await GetCommentByHashAsync(comment.ParentHash);
                if(parent != null && parent.Success && parent.Data != null)
                {
                    newComment.ParentId = parent.Data.Id;
                }
            }

            await _context.AddAsync(newComment);
            await _context.SaveChangesAsync();
            newComment.HashId = Hashing.Encode(newComment.Id);
            await _context.SaveChangesAsync();

            response.Data = newComment;
            return response;
        }

        public async Task<ServiceResponse<int>> CountCommentsByPostHashAsync(string postHash)
        {
            var response = new ServiceResponse<int>();

            var post = await _postService.GetPostByHashIdAsync(postHash);
            if (post == null || !post.Success || post.Data == null)
            {
                response.Success = false;
                response.Message = $"Post with ID '{postHash}' not found.";
                return response;
            }

            var comments = await _context.Comments.Include(c => c.Post).Where(c => c.Post.Id == post.Data.Id).ToListAsync();
            if (comments == null)
            {
                response.Success = false;
                response.Message = $"Comment with post ID '{postHash}' not found.";
                return response;
            }
            response.Data = comments.Count();
            return response;
        }

        public async Task<ServiceResponse<Comment>> GetCommentByHashAsync(string hash)
        {
            var response = new ServiceResponse<Comment>();
            var comment = await _context.Comments.Include(c => c.Post).ThenInclude(p => p.Board).ThenInclude(b => b.Owner).Where(c => c.HashId == hash).FirstOrDefaultAsync();
            if(comment == null)
            {
                response.Success = false;
                response.Message = $"Comment with ID '{hash}' not found.";
                return response;
            }
            response.Data = comment;
            return response;
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentsByParentHashAsync(string parentHash)
        {
            var response = new ServiceResponse<List<Comment>>();

            var parent = await GetCommentByHashAsync(parentHash);
            if(parent == null || !parent.Success || parent.Data == null)
            {
                response.Success = false;
                response.Message = $"Comment with ID '{parentHash}' not found.";
                return response;
            }

            var comments = await _context.Comments.Include(c => c.Post).ThenInclude(p => p.Board).ThenInclude(b => b.Owner).Where(c => c.ParentId == parent.Data.Id).ToListAsync();
            if (comments == null || !comments.Any())
            {
                response.Success = false;
                response.Message = $"Comment with parent ID '{parentHash}' not found.";
                return response;
            }
            response.Data = comments;
            return response;
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentsByPostHashAsync(string postHash)
        {
            var response = new ServiceResponse<List<Comment>>();

            var post = await _postService.GetPostByHashIdAsync(postHash);
            if (post == null || !post.Success || post.Data == null)
            {
                response.Success = false;
                response.Message = $"Post with ID '{postHash}' not found.";
                return response;
            }

            var comments = await _context.Comments.Include(c => c.Post).ThenInclude(p => p.Board).ThenInclude(b => b.Owner).Where(c => c.Post.Id == post.Data.Id && c.ParentId == 0).ToListAsync();
            if (comments == null || !comments.Any())
            {
                response.Success = false;
                response.Message = $"Comment with post ID '{postHash}' not found.";
                return response;
            }
            response.Data = comments;
            return response;
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentsByAuthorAsync(Guid guid)
        {
            var response = new ServiceResponse<List<Comment>>();

            var user = await _userService.GetUserByGuidAsync(guid);
            if (user == null || !user.Success || user.Data == null)
            {
                response.Success = false;
                response.Message = $"User with ID '{guid}' not found.";
                return response;
            }

            var comments = await _context.Comments.Include(c => c.Post).ThenInclude(p => p.Board).ThenInclude(b => b.Owner).Where(c => c.AuthorId == user.Data.Id).ToListAsync();
            if (comments == null || !comments.Any())
            {
                response.Success = false;
                response.Message = $"Comment with post ID '{guid}' not found.";
                return response;
            }
            response.Data = comments;
            return response;
        }

        public async Task<ServiceResponse<Comment>> EditCommentAsync(CommentEdit edit)
        {
            var response = new ServiceResponse<Comment>();
            var user = await _userService.GetUserByTokenAsync(edit.UserToken);
            if (user == null || !user.Success || user.Data == null)
            {
                response.Success = false;
                response.Message = $"User not found.";
                return response;
            }

            var comment = await _context.Comments.FindAsync(edit.CommentId);
            if (comment == null || comment.AuthorId != user.Data.Id)
            {
                response.Success = false;
                response.Message = $"Comment not found.";
                return response;
            }

            if(comment.Content == edit.NewContent)
            {
                response.Data = comment;
                return response;
            }

            comment.Content = edit.NewContent;
            comment.Edited = true;
            comment.DateEdited = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            response.Data = comment;
            return response;
        }
    }
}
