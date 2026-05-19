namespace Podcast.DTOs
{
    public class UpdateEpisodeDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public string? UrlAudio { get; set; }
        public byte[]? AudioData { get; set; }
    }
}
