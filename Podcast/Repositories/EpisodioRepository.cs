using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;
namespace Podcast.Repositories
{
    public class EpisodioRepository
    {
        private readonly string _connectionString;

        public EpisodioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> InsertAsync(Episodio episodio)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_InsertEpisodio",
                new
                {
                    episodio.IdPodcast,
                    episodio.Titulo,
                    episodio.Descripcion,
                    episodio.Duracion,
                    episodio.UrlAudio,
                    episodio.FechaPublicacion,
                    episodio.AudioGuid,
                    episodio.AudioData
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Episodio>> GetByPodcastAsync(int idPodcast)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Episodio>(
                "SP_GET_EPISODIOS_BY_PODCAST",
                new { id_podcast = idPodcast },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<Episodio?> GetAudioAsync(int idEpisodio)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Episodio>(
                "SP_GET_AUDIO",
                new { id_episodio = idEpisodio },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Episodio>> SearchAsync(string? keyword, string? categoria)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Episodio>(
                "SP_SEARCH_EPISODIOS",
                new { keyword, categoria },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
    }
}
