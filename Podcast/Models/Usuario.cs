namespace Podcast.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Name { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }

    }
}   