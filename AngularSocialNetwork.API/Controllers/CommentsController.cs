using AngularSocialNetwork.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularSocialNetwork.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentsRepo _repo;

        public CommentsController(ICommentsRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetComments(int? postId)
        {
            try
            {
                if (!postId.HasValue)
                {
                    throw new Exception("No post id.");
                }
                return Ok(_repo.GetComments(postId.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}