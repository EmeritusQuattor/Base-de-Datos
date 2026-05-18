using Microsoft.EntityFrameworkCore;
using Podcast.Models;
namespace Podcast
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserC> Usuarios { get; set; }
        public DbSet<PodcastModel> Podcasts { get; set; } 
        public DbSet<Episode> Episodios { get; set; }
        public DbSet<Reproduction> Reproducciones { get; set; }
        public DbSet<Category> Categorías { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserC>().ToTable("USUARIO");
            modelBuilder.Entity<PodcastModel>().ToTable("PODCAST");
            modelBuilder.Entity<Episode>().ToTable("EPISODIO");
            modelBuilder.Entity<Reproduction>().ToTable("REPRODUCCION");
            modelBuilder.Entity<Category>().ToTable("CATEGORIA");

            modelBuilder.Entity<UserC>().HasKey(u => u.IdUser);
            modelBuilder.Entity<PodcastModel>().HasKey(p => p.IdPodcast);
            modelBuilder.Entity<Episode>().HasKey(e => e.IdEpisode);
            modelBuilder.Entity<Category>().HasKey(c => c.IdCategory);
            modelBuilder.Entity<Reproduction>().HasKey(r => r.IdReproduction);
            // Column name mappings
            modelBuilder.Entity<UserC>().Property(u => u.IdUser).HasColumnName("id_usuario");
            modelBuilder.Entity<UserC>().Property(u => u.Name).HasColumnName("nombre");
            modelBuilder.Entity<UserC>().Property(u => u.User).HasColumnName("usuario");
            modelBuilder.Entity<UserC>().Property(u => u.Password).HasColumnName("contrasena");
            modelBuilder.Entity<UserC>().Property(u => u.Register).HasColumnName("fecha_registro");

            modelBuilder.Entity<PodcastModel>().Property(p => p.IdPodcast).HasColumnName("id_podcast");
            modelBuilder.Entity<PodcastModel>().Property(p => p.IdUser).HasColumnName("id_usuario");
            modelBuilder.Entity<PodcastModel>().Property(p => p.Title).HasColumnName("titulo");
            modelBuilder.Entity<PodcastModel>().Property(p => p.Description).HasColumnName("descripcion");
            modelBuilder.Entity<PodcastModel>().Property(p => p.Portrait).HasColumnName("imagen_portada");
            modelBuilder.Entity<PodcastModel>().Property(p => p.CreationTime).HasColumnName("fecha_creacion");

            modelBuilder.Entity<Episode>().Property(e => e.IdEpisode).HasColumnName("id_episodio");
            modelBuilder.Entity<Episode>().Property(e => e.IdPodcast).HasColumnName("id_podcast");
            modelBuilder.Entity<Episode>().Property(e => e.Title).HasColumnName("titulo");
            modelBuilder.Entity<Episode>().Property(e => e.Description).HasColumnName("descripcion");
            modelBuilder.Entity<Episode>().Property(e => e.Duration).HasColumnName("duracion");
            modelBuilder.Entity<Episode>().Property(e => e.UrlAudio).HasColumnName("url_audio");
            modelBuilder.Entity<Episode>().Property(e => e.PublishTime).HasColumnName("fecha_publicacion");
            modelBuilder.Entity<Episode>().Property(e => e.AudioGuid).HasColumnName("audio_guid");
            modelBuilder.Entity<Episode>().Property(e => e.AudioData).HasColumnName("audio_data");

            modelBuilder.Entity<Category>().Property(c => c.IdCategory).HasColumnName("id_categoria");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("nombre");
            modelBuilder.Entity<Category>().Property(c => c.Description).HasColumnName("descripcion");

            modelBuilder.Entity<Reproduction>().Property(r => r.IdReproduction).HasColumnName("id_reproduccion");
            modelBuilder.Entity<Reproduction>().Property(r => r.IdEpisode).HasColumnName("id_episodio");
            modelBuilder.Entity<Reproduction>().Property(r => r.IdUser).HasColumnName("id_usuario");
            modelBuilder.Entity<Reproduction>().Property(r => r.ReproductionTime).HasColumnName("fecha_reproduccion");
            modelBuilder.Entity<Reproduction>().Property(r => r.TimeHeard).HasColumnName("segundos_escuchados");
        }

    }
}
