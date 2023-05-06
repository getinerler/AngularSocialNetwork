using AngularSocialNetwork.API.Dtos.Users;
using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data
{
    public interface IAuthRepo
    {
        User Register(UserForRegisterDto userForRegisterDto);
        User Login(string username, string password);
        bool UserExists(string username);
    }
}