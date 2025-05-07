using UserRoleManagementApi.Models;

namespace UserRoleManagementApi.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> CreateRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int id);
        Task AssignRoleToUser(int userId, int roleId);
        Task RemoveRoleFromUser(int userId, int roleId);
    }
}