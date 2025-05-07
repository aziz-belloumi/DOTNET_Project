using Microsoft.EntityFrameworkCore;
using UserRoleManagementApi.Models;
using UserRoleManagementApi.Models.Data;
using UserRoleManagementApi.Services.Interfaces;

namespace UserRoleManagementApi.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _context.Posts
                .Include(p => p.User) // Include user data
                .ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> CreatePost(Post post)
        {
            // Verify the user exists
            var user = await _context.Users.FindAsync(post.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task UpdatePost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(int userId)
        {
            return await _context.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .ToListAsync();
        }
    }
}