using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;

namespace Podcast.Repositories
{
    public class ReproduccionRepository
    {
        private readonly string _connectionString;

        public ReproduccionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> InsertAsync(Reproduccion reproduccion)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_INSERT_REPRODUCCION",
                new
                {
                    reproduccion.IdEpisodio,
                    reproduccion.IdUsuario,
                    reproduccion.SegundosEscuchados
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Reproduccion>> GetByUsuarioAsync(int idUsuario)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Reproduccion>(
                @"SELECT id_reproduccion AS IdReproduccion, id_episodio AS IdEpisodio,
                  id_usuario AS IdUsuario, fecha_reproduccion AS FechaReproduccion,
                  segundos_escuchados AS SegundosEscuchados
                  FROM REPRODUCCION WHERE id_usuario = @IdUsuario
                  ORDER BY fecha_reproduccion DESC",
                new { IdUsuario = idUsuario }
            );
        }
    }
}