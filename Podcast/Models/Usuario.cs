namespace Podcast.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}
