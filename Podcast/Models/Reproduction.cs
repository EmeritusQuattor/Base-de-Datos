namespace Podcast.Models
{
    public class Reproduction
    {
        public int IdReproduction { get; set; }
        public int IdEpisode { get; set; }
        public int IdUser { get; set; }
        public DateTime ReproductionTime { get; set; }
        public int TimeHeard { get; set; }
    }
}
