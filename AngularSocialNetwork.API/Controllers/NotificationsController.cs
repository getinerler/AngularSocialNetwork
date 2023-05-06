using AngularSocialNetwork.API.Data;
using AngularSocialNetwork.API.Dtos.Notifications;
using AngularSocialNetwork.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularSocialNetwork.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsRepo _repo;

        public NotificationsController(INotificationsRepo repo)
        {
            _repo = repo;
        }

        public IActionResult GetNotifications(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("No user id.");
                }

                List<Notification> notifications = _repo.GetNotifications(id.Value);

                List<NotificationForListDto> notificationsDto =
                notifications
                    .Select(x => new NotificationForListDto()
                    {
                        Text = x.Text,
                        Date = x.CreatedDate
                    })
                    .ToList();

                return Ok(notificationsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}