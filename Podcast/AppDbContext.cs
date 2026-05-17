using Microsoft.EntityFrameworkCore;
using Podcast.Models;
namespace Podcast
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PodcastModel> Podcasts { get; set; } 
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Reproduccion> Reproducciones { get; set; }
        public DbSet<Categoria> Categorías { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("USUARIO");
            modelBuilder.Entity<PodcastModel>().ToTable("PODCAST");
            modelBuilder.Entity<Episodio>().ToTable("EPISODIO");
            modelBuilder.Entity<Reproduccion>().ToTable("REPRODUCCION");
            modelBuilder.Entity<Categoria>().ToTable("CATEGORIA");

            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<PodcastModel>().HasKey(p => p.IdPodcast);
            modelBuilder.Entity<Episodio>().HasKey(e => e.IdEpisodio);
            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            modelBuilder.Entity<Reproduccion>().HasKey(r => r.IdReproduccion);
            // Column name mappings
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<Usuario>().Property(u => u.Name).HasColumnName("nombre");
            modelBuilder.Entity<Usuario>().Property(u => u.User).HasColumnName("usuario");
            modelBuilder.Entity<Usuario>().Property(u => u.Contrasena).HasColumnName("contrasena");
            modelBuilder.Entity<Usuario>().Property(u => u.FechaRegistro).HasColumnName("fecha_registro");

            modelBuilder.Entity<PodcastModel>().Property(p => p.IdPodcast).HasColumnName("id_podcast");
            modelBuilder.Entity<PodcastModel>().Property(p => p.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<PodcastModel>().Property(p => p.Titulo).HasColumnName("titulo");
            modelBuilder.Entity<PodcastModel>().Property(p => p.Descripcion).HasColumnName("descripcion");
            modelBuilder.Entity<PodcastModel>().Property(p => p.ImagenPortada).HasColumnName("imagen_portada");
            modelBuilder.Entity<PodcastModel>().Property(p => p.FechaCreacion).HasColumnName("fecha_creacion");

            modelBuilder.Entity<Episodio>().Property(e => e.IdEpisodio).HasColumnName("id_episodio");
            modelBuilder.Entity<Episodio>().Property(e => e.IdPodcast).HasColumnName("id_podcast");
            modelBuilder.Entity<Episodio>().Property(e => e.Titulo).HasColumnName("titulo");
            modelBuilder.Entity<Episodio>().Property(e => e.Descripcion).HasColumnName("descripcion");
            modelBuilder.Entity<Episodio>().Property(e => e.Duracion).HasColumnName("duracion");
            modelBuilder.Entity<Episodio>().Property(e => e.UrlAudio).HasColumnName("url_audio");
            modelBuilder.Entity<Episodio>().Property(e => e.FechaPublicacion).HasColumnName("fecha_publicacion");
            modelBuilder.Entity<Episodio>().Property(e => e.AudioGuid).HasColumnName("audio_guid");
            modelBuilder.Entity<Episodio>().Property(e => e.AudioData).HasColumnName("audio_data");

            modelBuilder.Entity<Categoria>().Property(c => c.IdCategoria).HasColumnName("id_categoria");
            modelBuilder.Entity<Categoria>().Property(c => c.Name).HasColumnName("nombre");
            modelBuilder.Entity<Categoria>().Property(c => c.Descripcion).HasColumnName("descripcion");

            modelBuilder.Entity<Reproduccion>().Property(r => r.IdReproduccion).HasColumnName("id_reproduccion");
            modelBuilder.Entity<Reproduccion>().Property(r => r.IdEpisodio).HasColumnName("id_episodio");
            modelBuilder.Entity<Reproduccion>().Property(r => r.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<Reproduccion>().Property(r => r.FechaReproduccion).HasColumnName("fecha_reproduccion");
            modelBuilder.Entity<Reproduccion>().Property(r => r.SegundosEscuchados).HasColumnName("segundos_escuchados");
        }

    }
}
