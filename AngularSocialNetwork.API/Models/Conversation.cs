namespace AngularSocialNetwork.API.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}