namespace AngularSocialNetwork.API.Helper
{
    public static class UrlCreate
    {
        public static string baseUrl = "";

        public static string GetPhotoUrl(Guid guid) 
        {
            return "https://localhost:7210/profilePhotos/" + guid + ".jpg";
        }
    }
}