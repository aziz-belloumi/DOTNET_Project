using Microsoft.Extensions.Hosting;
using System.Data;

namespace UserRoleManagementApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } 
        public List<Post>? Posts { get; set; }
        public List<Role>? Roles { get; set; } 
    }
}