using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularSocialNetwork.API.Data;
using AngularSocialNetwork.API.Dtos.Auth;
using AngularSocialNetwork.API.Dtos.Users;
using AngularSocialNetwork.API.Helper;
using AngularSocialNetwork.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AngularSocialNetwork.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepo repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (_repo.UserExists(userForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            User createdUser = _repo.Register(userForRegisterDto);

            UserForProfileDto userForProfile = new UserForProfileDto()
            {
                Id = createdUser.UserId,
                Username = createdUser.Username,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email,
                Bio = createdUser.Bio,
                Link = createdUser.ProfileLink,
                Photo = "https://localhost:7048/profilePhotos/" + createdUser.ProfilePhoto + ".jpg",
                Date = createdUser.CreatedDate,
            };

            return CreatedAtRoute("GetUser", new
            {
                controller = "Users",
                id = createdUser.UserId
            }, userForProfile);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
        {
            try
            {
                User userFromRepo = _repo.Login(
                    userForLoginDto.Username.ToLower(),
                    userForLoginDto.Password);

                if (userFromRepo == null)
                {
                    return Unauthorized();
                }

                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.FirstName)
                };

                string settingsToken = _config.GetSection("AppSettings:Token").Value;

                if (string.IsNullOrEmpty(settingsToken))
                {
                    throw new Exception("No setting token.");
                }

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settingsToken));

                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

                UserForProfileDto userForProfile = new UserForProfileDto()
                {
                    Id = userFromRepo.UserId,
                    Username = userFromRepo.Username,
                    FirstName = userFromRepo.FirstName,
                    LastName = userFromRepo.LastName,
                    Email = userFromRepo.Email,
                    Bio = userFromRepo.Bio,
                    Link = userFromRepo.ProfileLink,
                    Photo = UrlCreate.GetPhotoUrl(userFromRepo.ProfilePhoto),
                    Date = userFromRepo.CreatedDate,
                };

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user = userForProfile
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}