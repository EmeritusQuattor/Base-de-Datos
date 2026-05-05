namespace Podcast.Models
{
    public class PodcastModel
    {
        public int IdPodcast { get; set; }
        public int IdUsuario { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? ImagenPortada { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
