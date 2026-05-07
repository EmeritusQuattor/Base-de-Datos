using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;

namespace Podcast.Repositories
{
    public class PodcastRepository
    {
        private readonly string _connectionString;
        public PodcastRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> InsertAsync(PodcastModel podcast)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_INSERT_PODCAST",
                new { podcast.IdUsuario, podcast.Titulo, podcast.Descripcion, podcast.ImagenPortada },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<PodcastModel>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<PodcastModel>(
                @"SELECT id_podcast AS IdPodcast, id_usuario AS IdUsuario, 
                  titulo AS Titulo, descripcion AS Descripcion, 
                  imagen_portada AS ImagenPortada, fecha_creacion AS FechaCreacion 
                  FROM PODCAST"
            );
        }
        public async Task<IEnumerable<PodcastModel>> GetByUsuarioAsync(int idUsuario)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<PodcastModel>(
                @"SELECT id_podcast AS IdPodcast, id_usuario AS IdUsuario,
                  titulo AS Titulo, descripcion AS Descripcion,
                  imagen_portada AS ImagenPortada, fecha_creacion AS FechaCreacion
                  FROM PODCAST WHERE id_usuario = @IdUsuario",
                new { IdUsuario = idUsuario }
            );
        }
    }
}