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
                .Include(p => p.User)
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
            var user = await _context.Users.FindAsync(post.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            post.User = user;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task UpdatePost(Post updatedPost)
        {
            var existingPost = await _context.Posts.FindAsync(updatedPost.Id);

            if (existingPost == null)
            {
                throw new ArgumentException("Post not found");
            }

            // Update post fields
            existingPost.Title = updatedPost.Title;
            existingPost.Content = updatedPost.Content;

            // Update user if changed
            if (existingPost.UserId != updatedPost.UserId)
            {
                var newUser = await _context.Users.FindAsync(updatedPost.UserId);
                if (newUser == null)
                {
                    throw new ArgumentException("User not found");
                }

                existingPost.UserId = updatedPost.UserId;
                existingPost.User = newUser;
            }

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
    }
}
