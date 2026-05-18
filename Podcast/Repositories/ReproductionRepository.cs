using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;

namespace Podcast.Repositories
{
    public class ReproductionRepository
    {
        private readonly string _connectionString;

        public ReproductionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> InsertAsync(Reproduction reproduccion)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_INSERT_REPRODUCCION",
                new
                {
                    reproduccion.IdEpisode,
                    reproduccion.IdUser,
                    reproduccion.TimeHeard
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Reproduction>> GetByUsuarioAsync(int idUsuario)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Reproduction>(
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