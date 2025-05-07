using Microsoft.AspNetCore.Mvc;
using UserRoleManagementApi.Models;
using UserRoleManagementApi.Services.Interfaces;

namespace UserRoleManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return Ok(await _postService.GetAllPosts());
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            try
            {
                var createdPost = await _postService.CreatePost(post);
                return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            await _postService.UpdatePost(post);
            return NoContent();
        }

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePost(id);
            return NoContent();
        }

        // GET: api/users/5/posts
        [HttpGet("~/api/users/{userId}/posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUser(int userId)
        {
            return Ok(await _postService.GetPostsByUserId(userId));
        }
    }
}