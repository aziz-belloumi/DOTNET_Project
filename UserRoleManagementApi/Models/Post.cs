using System.Text.Json.Serialization;

namespace UserRoleManagementApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}