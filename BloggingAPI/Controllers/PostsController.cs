using BloggingAPI.Business.Interfaces;
using BloggingAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BloggingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController(ILogger<PostsController> logger, IBlogPostService blogPostService) : ControllerBase
    {
        private readonly ILogger<PostsController> _logger = logger;
        private readonly IBlogPostService _blogPostService = blogPostService;

        /// <summary>
        /// Get all blog posts from database
        /// </summary>
        /// <returns>A list of all saved blog posts</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await _blogPostService.GetAll();
            if (result.Any())
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Creates a new blog post
        /// </summary>
        /// <param name="post">Post to be saved</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post(BlogPost post)
        {
            try
            {
                await _blogPostService.Create(post);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);                
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets a specific blog post
        /// </summary>
        /// <param name="id">Blog post Id to be retrieved</param>
        /// <returns>Requested blog post</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _blogPostService.GetById(id);
            if (result != null)
                return Ok(result);

            return BadRequest($"Blog post {id} doesn't exist");
        }

        /// <summary>
        /// Post one or more comments to a specific blog post
        /// </summary>
        /// <param name="id">Blog post Id</param>
        /// <param name="comments">List of comments to be added</param>
        /// <returns></returns>
        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostComments(int id, IEnumerable<Comment> comments)
        {
            try
            {
                await _blogPostService.PostComments(id, comments);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
