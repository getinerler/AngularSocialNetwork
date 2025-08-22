using AngularSocialNetwork.API.Data;
using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Hubs;
using AngularSocialNetwork.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AngularSocialNetwork.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepo _postRepo;
        private readonly IHubContext<AngularHub> _client;

        public PostsController(IPostsRepo postRepo, IHubContext<AngularHub> client)
        {
            _postRepo = postRepo;
            _client = client;
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

        [HttpPost("RepostPost")]
        public IActionResult RepostPost([FromBody] PostLikeDto req)
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

                return Ok(_postRepo.RepostPost(req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LikePost")]
        public async Task<IActionResult> LikePost([FromBody] PostLikeDto req)
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

                PostLikeResultDto res = _postRepo.LikePost(req);

                if (res.Liked)
                {
                    string connId = AngularHub.UserConnectionIds[res.PostUserId]?.ToString();
                    await _client.Clients.Client(connId).SendAsync("NewNotification", 2);
                    Console.WriteLine("not: " + connId);
                }
                return Ok();
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

        [HttpDelete]
        public IActionResult DeletePost(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("No post id.");
                }
                _postRepo.DeletePost(id.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}