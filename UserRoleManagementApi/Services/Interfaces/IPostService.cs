using UserRoleManagementApi.Models;

namespace UserRoleManagementApi.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostById(int id);
        Task<Post> CreatePost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int id);
        Task<IEnumerable<Post>> GetPostsByUserId(int userId); // Get all posts for a specific user
    }
}