using AngularSocialNetwork.API.Data;
using AngularSocialNetwork.API.Dtos.Posts;
using Microsoft.AspNetCore.Mvc;

namespace AngularSocialNetwork.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepo _postRepo;

        public PostsController(IPostsRepo postRepo)
        {
            _postRepo = postRepo;
        }

        [HttpGet(Name = "GetMessages")]
        public IEnumerable<PostForFeedDto> Get(int? userId)
        {
            if (!userId.HasValue)
            {
                Console.WriteLine("Common feed.");
            }

            return _postRepo.GetPosts(userId ?? -1);
        }

        [HttpPost("SaveNewPost")]
        public IActionResult SaveNewPost([FromBody] PostAddDto postAddDto)
        {
            try
            {
                _postRepo.SaveNewPost(postAddDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}