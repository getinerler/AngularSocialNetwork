namespace AngularSocialNetwork.API.Dtos.Posts
{
    public class PostForFeedDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public int LikeCount { get; set; }
        public int RetweetCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime Date { get; set; }
        public bool Liked { get; set; }
    }
}