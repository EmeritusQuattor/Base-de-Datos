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
            this._connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> InsertAsync(Reproduction reproduction)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.ExecuteScalarAsync<int>(
                "SP_INSERT_REPRODUCCION",
                new
                {
                    id_episodio = reproduction.IdEpisode,
                    id_usuario = reproduction.IdUser,
                    segundos_escuchados = reproduction.TimeHeard
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Reproduction>> GetByUserAsync(int idUser)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            return await conn.QueryAsync<Reproduction>(
                @"SELECT id_reproduccion AS IdReproduction, id_episodio AS IdEpisode,
                  id_usuario AS IdUser, fecha_reproduccion AS ReproductionTime,
                  segundos_escuchados AS TimeHeard
                  FROM REPRODUCCION WHERE id_usuario = @IdUser
                  ORDER BY fecha_reproduccion DESC",
                new { IdUser = idUser }
            );
        }
    }
}