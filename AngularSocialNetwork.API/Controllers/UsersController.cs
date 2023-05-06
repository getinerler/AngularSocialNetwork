using AngularSocialNetwork.API.Data;
using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace AngularSocialNetwork.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repo;

        public UsersController(IUsersRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("GetProfileInfo")]
        public IActionResult GetProfileInfo(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("No userId.");
                }
                UserForProfileDto userForProfile = _repo.GetProfileInfo(id.Value);
                return Ok(userForProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPosts")]
        public IActionResult GetPosts(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("No userId.");
                }
                List<PostForFeedDto> postsForMainPageDto = _repo.GetUserPosts(id.Value);
                return Ok(postsForMainPageDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}