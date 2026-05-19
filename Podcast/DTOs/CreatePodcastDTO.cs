namespace Podcast.DTOs
{
    public class CreatePodcastDTO
    {
        public int IdUser { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Portrait { get; set; }
    }
}
