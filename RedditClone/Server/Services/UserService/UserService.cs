using System.Xml.Linq;

namespace RedditClone.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<User>> GetUserByEmailAsync(string email)
        {
            ServiceResponse<User> response = new();
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(user == null)
            {
                response.Success = false;
                response.Message = $"User with email '{email}' not found.";
                return response;
            }
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<User>> GetUserByGuidAsync(Guid guid)
        {
            ServiceResponse<User> response = new();
            var user = await _context.Users.Where(u => u.Guid == guid).FirstOrDefaultAsync();
            if (user == null)
            {
                response.Success = false;
                response.Message = $"User with GUID '{guid}' not found.";
                return response;
            }
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<User>> GetUserByIdAsync(int id)
        {
            ServiceResponse<User> response = new();
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                response.Success = false;
                response.Message = $"User with ID '{id}' not found.";
                return response;
            }
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<User>> GetUserByNameAsync(string name)
        {
            ServiceResponse<User> response = new();
            var user = await _context.Users.Where(u => u.Username == name).FirstOrDefaultAsync();
            if (user == null)
            {
                response.Success = false;
                response.Message = $"User with the name '{name}' not found.";
                return response;
            }
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<User>> GetUserByTokenAsync(string token)
        {
            ServiceResponse<User> response = new();
            var userData = await _context.UserData.Where(d => d.Token == token).FirstOrDefaultAsync();
            if (userData == null)
            {
                response.Success = false;
                response.Message = $"User not found.";
                return response;
            }
            var user = await _context.Users.Where(u => u.Id == userData.UserId).FirstOrDefaultAsync();
            if (user == null)
            {
                response.Success = false;
                response.Message = $"User not found.";
                return response;
            }
            response.Data = user;
            return response;
        }
    }
}
