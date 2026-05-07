using Dapper; using Microsoft.Data.SqlClient; using Podcast.Models;
namespace Podcast.Repositories
{
    public class UsuarioRepositories
    {
        public class UsuarioRepository
        {
            private readonly string _connectionString;
            public UsuarioRepository(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection");
            }
            public async Task<int> InsertAsync(Usuario usuario)
            {
                using var conn = new SqlConnection(_connectionString);
                var result = await conn.ExecuteScalarAsync<int>(
                "SP_INSERT_USUARIO",
                new { usuario.Nombre, usuario.Email, usuario.Contrasena},
                commandType: System.Data.CommandType.StoredProcedure
                );
                return result;
            }
            public async Task<IEnumerable<Usuario>> GetAllAsync()
            {
                using var conn = new SqlConnection(_connectionString);
                return await conn.QueryAsync<Usuario>(
                    "SELECT id_usuario AS IdUsuario, nombre AS Nombre, email AS Email, fecha_registro AS FechaRegistro FROM USUARIO"
                );
            }

            public async Task<Usuario?> GetByIdAsync(int id)
            {
                using var conn = new SqlConnection(_connectionString);
                return await conn.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT id_usuario AS IdUsuario, nombre AS Nombre, email AS Email, fecha_registro AS FechaRegistro FROM USUARIO WHERE id_usuario = @Id",
                    new { Id = id }
                );
            }



        }
    }
}
