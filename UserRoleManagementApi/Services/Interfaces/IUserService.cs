using UserRoleManagementApi.Models;

namespace UserRoleManagementApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<object>> GetAllUsers();
        Task<object> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<IEnumerable<object>> GetUserPosts(int userId);
        Task<IEnumerable<object>> GetUserRoles(int userId);
    }
}