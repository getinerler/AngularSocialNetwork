using AngularSocialNetwork.API.Dtos.Users;
using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class AuthRepoTest : IAuthRepo
    {
        public bool UserExists(string username)
        {
            return DatabaseContextTest.Users.Any(x => x.Username == username);
        }

        public User Login(string username, string password)
        {
            User user = DatabaseContextTest.Users.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public User Register(UserForRegisterDto userForRegisterDto)
        {
            CreatePasswordHash(userForRegisterDto.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);


            User user = new User()
            {
                Username = userForRegisterDto.Username,
                FirstName = userForRegisterDto.Firstname,
                LastName = userForRegisterDto.Lastname,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Email = userForRegisterDto.Email,
                Bio = string.Empty,
                ProfileLink = string.Empty,
                ProfilePhoto = Guid.Empty,
                HeaderPhoto = Guid.Empty,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            DatabaseContextTest.Users.Add(user);

            return user;
        }

        private void CreatePasswordHash(
                  string password,
                  out byte[] passwordHash,
                  out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(
            string password,
            byte[] passwordHash,
            byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}