using Microsoft.EntityFrameworkCore;
using UserRoleManagementApi.Models;
using UserRoleManagementApi.Models.Data;
using UserRoleManagementApi.Services.Interfaces;

namespace UserRoleManagementApi.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles
                .Include(r => r.Users)
                .ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task UpdateRole(Role updatedRole)
        {
            var existingRole = await _context.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == updatedRole.Id);

            if (existingRole == null)
            {
                throw new ArgumentException("Role not found");
            }

            existingRole.Name = updatedRole.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await _context.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role != null)
            {
                role.Users.Clear(); // detach from users if needed
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignRoleToUser(int userId, int roleId)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var role = await _context.Roles.FindAsync(roleId);

            if (user == null || role == null)
            {
                throw new ArgumentException("User or Role not found");
            }

            if (!user.Roles.Any(r => r.Id == roleId))
            {
                user.Roles.Add(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveRoleFromUser(int userId, int roleId)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var roleToRemove = user.Roles.FirstOrDefault(r => r.Id == roleId);
            if (roleToRemove != null)
            {
                user.Roles.Remove(roleToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
