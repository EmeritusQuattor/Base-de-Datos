namespace Podcast.DTOs
{
    public class EpisodeDTO
    {
        public int IdEpisode { get; set; }
        public int IdPodcast { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Duration { get; set; }
        public string? UrlAudio { get; set; }
        public DateTime PublishTime { get; set; }
        public Guid AudioGuid { get; set; }
    }
}
