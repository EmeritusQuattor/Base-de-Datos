using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;

namespace Podcast.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> InsertAsync(UserC user)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO USUARIO (nombre, usuario, contrasena)
                  VALUES (@nombre, @usuario, @contrasena);
                  SELECT SCOPE_IDENTITY();",
                new { nombre = user.Name, usuario = user.User, contrasena = user.Password }
            );
        }

        public async Task<IEnumerable<UserC>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<UserC>(
                @"SELECT id_usuario AS IdUser, nombre AS Name, 
                  usuario AS User, fecha_registro AS Register 
                  FROM USUARIO"
            );
        }

        public async Task<UserC?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<UserC>(
                @"SELECT id_usuario AS IdUser, nombre AS Name,
                  usuario AS User, fecha_registro AS Register
                  FROM USUARIO WHERE id_usuario = @Id",
                new { Id = id }
            );
        }

        public async Task<UserC?> LoginAsync(string usuario, string password)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<UserC>(
                @"SELECT id_usuario AS IdUser, nombre AS Name,
                  usuario AS User, fecha_registro AS Register
                  FROM USUARIO WHERE usuario = @usuario AND contrasena = @password",
                new { usuario, password }
            );
        }
    }
}