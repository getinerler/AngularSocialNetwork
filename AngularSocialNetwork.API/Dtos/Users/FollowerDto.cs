namespace AngularSocialNetwork.API.Dtos.Users
{
    public class FollowerDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public bool Following { get; set; }
    }
}