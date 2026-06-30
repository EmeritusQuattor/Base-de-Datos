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
            this._connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> InsertAsync(PodcastModel podcast)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_INSERT_PODCAST",
                new
                {
                    id_usuario = podcast.IdUser,
                    titulo = podcast.Title,
                    descripcion = podcast.Description,
                    imagen_portada = podcast.Portrait
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<PodcastModel>> GetAllAsync()
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.QueryAsync<PodcastModel>(
                @"SELECT id_podcast AS IdPodcast, id_usuario AS IdUser,
                  titulo AS Title, descripcion AS Description,
                  imagen_portada AS Portrait, fecha_creacion AS CreationTime
                  FROM PODCAST"
            );
        }

        public async Task<IEnumerable<PodcastModel>> GetByUserAsync(int idUser)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.QueryAsync<PodcastModel>(
                @"SELECT id_podcast AS IdPodcast, id_usuario AS IdUser,
                  titulo AS Title, descripcion AS Description,
                  imagen_portada AS Portrait, fecha_creacion AS CreationTime
                  FROM PODCAST WHERE id_usuario = @IdUser",
                new { IdUser = idUser }
            );
        }
    }
}