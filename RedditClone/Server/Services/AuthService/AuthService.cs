using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RedditClone.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RedditClone.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> LoginAsync(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }
            var userData = await _context.UserData.Where(d => d.UserId == user.Id).FirstOrDefaultAsync();
            if (userData == null)
            {
                response.Success = false;
                response.Message = "User data not found.";
                return response;
            }
            if (!VerifyPasswordHash(password, userData.PasswordHash, userData.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
                return response;
            }
            else
            {
                if(!string.IsNullOrEmpty(userData.Token) && TokenIsValid(userData.Token))
                {
                    response.Data = userData.Token;
                }
                else
                {
                    var token = CreateToken(user);
                    response.Data = token;
                    userData.Token = token;
                    await _context.SaveChangesAsync();
                }
            }

            return response;
        }

        private bool TokenIsValid(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecToken = handler.ReadJwtToken(token);
            var exp = jwtSecToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;

            return tokenDate >= DateTime.UtcNow.ToUniversalTime();
        }

        public async Task<ServiceResponse<int>> RegisterAsync(UserRegister userRegister)
        {
            if (await UserExists(userRegister.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }
            var user = new User { Email = userRegister.Email, Username = userRegister.Email.Split('@')[0].Trim() };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            CreatePasswordHash(userRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var data = new UserData { UserId = user.Id, PasswordHash = passwordHash, PasswordSalt = passwordSalt };
            await _context.UserData.AddAsync(data);

            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful!" };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            var userData = await _context.UserData.Where(d => d.UserId == userId).FirstOrDefaultAsync();
            if (userData == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            if(!VerifyPasswordHash(oldPassword, userData.PasswordHash, userData.PasswordSalt))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            userData.PasswordHash = passwordHash;
            userData.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed" };
        }
    }
}
