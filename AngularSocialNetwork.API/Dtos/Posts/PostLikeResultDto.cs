namespace AngularSocialNetwork.API.Dtos.Posts
{
    public class PostLikeResultDto
    {
        public int LikeCount { get; set; }
        public int PostUserId { get; set; }
        public bool Liked { get; set; }
    }
}