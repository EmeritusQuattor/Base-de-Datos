using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;
namespace Podcast.Repositories
{
    public class EpisodeRepository
    {
        private readonly string _connectionString;

        public EpisodeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> InsertAsync(Episode episodio)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_InsertEpisodio",
                new
                {
                    episodio.IdPodcast,
                    episodio.Title,
                    episodio.Description,
                    episodio.Duration,
                    episodio.UrlAudio,
                    episodio.PublishTime,
                    episodio.AudioGuid,
                    episodio.AudioData
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Episode>> GetByPodcastAsync(int idPodcast)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Episode>(
                "SP_GET_EPISODIOS_BY_PODCAST",
                new { id_podcast = idPodcast },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<Episode?> GetAudioAsync(int idEpisodio)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Episode>(
                "SP_GET_AUDIO",
                new { id_episodio = idEpisodio },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Episode>> SearchAsync(string? keyword, string? categoria)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Episode>(
                "SP_SEARCH_EPISODIOS",
                new { keyword, categoria },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
    }
}
