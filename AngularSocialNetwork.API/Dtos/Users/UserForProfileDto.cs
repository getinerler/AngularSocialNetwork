namespace AngularSocialNetwork.API.Dtos.Users
{
    public class UserForProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public DateTime Date { get; set; }
    }
}