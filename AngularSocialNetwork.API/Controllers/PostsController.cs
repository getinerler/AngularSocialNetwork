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

        [HttpGet]
        public IEnumerable<PostForFeedDto> Get(int? userId)
        {
            if (!userId.HasValue)
            {
                Console.WriteLine("Common feed.");
            }

            return _postRepo.GetPosts(userId ?? -1);
        }

        [HttpGet("GetPostDetails")]
        public IActionResult GetPostDetails(int? feedId)
        {
            try
            {
                if (!feedId.HasValue)
                {
                    throw new Exception("No feed id.");
                }
                return Ok(_postRepo.GetPostDetailed(feedId.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LikePost")]
        public IActionResult LikePost([FromBody] PostLikeDto req)
        {
            try
            {
                if (!req.UserId.HasValue)
                {
                    throw new Exception("No user id.");
                }

                if (!req.FeedId.HasValue)
                {
                    throw new Exception("No feed id.");
                }

                return Ok( _postRepo.LikePost(req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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