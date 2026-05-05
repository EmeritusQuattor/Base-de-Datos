namespace Podcast.Models
{
    public class Reproduccion
    {
        public int IdReproduccion { get; set; }
        public int IdEpisodio { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaReproduccion { get; set; }
        public int SegundosEscuchados { get; set; }
    }
}
