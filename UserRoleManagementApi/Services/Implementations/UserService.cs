using Microsoft.EntityFrameworkCore;
using UserRoleManagementApi.Models;
using UserRoleManagementApi.Models.Data;
using UserRoleManagementApi.Services.Interfaces;

namespace UserRoleManagementApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.Posts)
                .Include(u => u.Roles)
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    Posts = u.Posts.Select(p => new
                    {
                        p.Id,
                        p.Title,
                        p.Content
                    }).ToList(),
                    Roles = u.Roles.Select(r => new
                    {
                        r.Id,
                        r.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<object> GetUserById(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Roles)
                .Include(u => u.Posts)
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    Posts = u.Posts.Select(p => new
                    {
                        p.Id,
                        p.Title,
                        p.Content
                    }).ToList(),
                    Roles = u.Roles.Select(r => new
                    {
                        r.Id,
                        r.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }


        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(updatedUser.Id);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                user.Roles.Clear();
                _context.Posts.RemoveRange(user.Posts);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<object>> GetUserPosts(int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }
            return await _context.Posts
              .Where(p => p.UserId == userId)
              .Select(p => new
              {
                  p.Id,
                  p.Title,
                  p.Content
              }).ToListAsync();

        }

        public async Task<IEnumerable<object>> GetUserRoles(int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }
            return await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles.Select(r => new
                {
                    r.Id,
                    r.Name
                }))
                .ToListAsync();
        }

    }
}
