using UserRoleManagementApi.Models;

namespace UserRoleManagementApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<IEnumerable<Post>> GetUserPosts(int userId);
        Task<IEnumerable<Role>> GetUserRoles(int userId);
    }
}