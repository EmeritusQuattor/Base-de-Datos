namespace Podcast.Models
{
    public class Episodio
    {
        public int IdEpisodio { get; set; }
        public int IdPodcast { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Duracion { get; set; }
        public string? UrlAudio { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public Guid AudioGuid { get; set; }
        public byte[]? AudioData { get; set; }
    }
}
