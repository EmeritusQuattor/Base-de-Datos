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
            this._connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> InsertAsync(Episode episode)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_INSERT_EPISODIO",
                new
                {
                    id_podcast = episode.IdPodcast,
                    titulo = episode.Title,
                    descripcion = episode.Description,
                    duracion = episode.Duration,
                    url_audio = episode.UrlAudio,
                    audio_data = episode.AudioData
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Episode>> GetByPodcastAsync(int idPodcast)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.QueryAsync<Episode>(
                "SP_GET_EPISODIOS_BY_PODCAST",
                new { id_podcast = idPodcast },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<Episode?> GetAudioAsync(int idEpisode)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.QueryFirstOrDefaultAsync<Episode>(
                "SP_GET_AUDIO",
                new { id_episodio = idEpisode },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Episode>> SearchAsync(string? keyword, string? category)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.QueryAsync<Episode>(
                "SP_SEARCH_EPISODIOS",
                new { keyword, categoria = category },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
    }
}