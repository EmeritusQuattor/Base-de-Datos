namespace Podcast.DTOs
{
    public class CategoryDTO
    {
        public int IdCategory { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
