namespace Podcast.DTOs
{
    public class CreateEpisodeDTO
    {
        public int IdPodcast { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Duration { get; set; }
        public string? UrlAudio { get; set; }
        public byte[]? AudioData { get; set; }
    }
}
