using UserRoleManagementApi.Models;

namespace UserRoleManagementApi.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<object>> GetAllPosts();
        Task<object> GetPostById(int id);
        Task<Post> CreatePost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int id);
    }
}