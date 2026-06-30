namespace Podcast.Models
{
    public class UserC
    {
        public int IdUser { get; set; }
        public string Name { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Register { get; set; }
    }
}