namespace Podcast.Models
{
    public class Category
    {
        public int IdCategory { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

    }
}
