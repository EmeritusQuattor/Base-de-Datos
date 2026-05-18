namespace Podcast.Models
{
    public class PodcastModel
    {
        public int IdPodcast { get; set; }
        public int IdUser { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Portrait { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
