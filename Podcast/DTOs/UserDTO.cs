namespace Podcast.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string Name { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public DateTime Register { get; set; }
    }
}
