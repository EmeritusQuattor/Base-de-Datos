using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;

namespace Podcast.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("DefaultConnection");
        public async Task<int> InsertAsync(Usuario usuario)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>(

                @"INSERT INTO Usuarios (Name, User, Contrasena)
                    VALUES (@Name, @User, @Contrasena);
                    SELECT SCOPE_IDENTITY();",
                new { Nombre = usuario.Name, Usuario = usuario.User, Contraseña = usuario.Contrasena }
            );
        }
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Usuario>(
                 "SELECT id_usuario AS IdUsuario, nombre AS Nombre, usuario AS Usuario, fecha_registro AS FechaRegistro FROM USUARIO");
        }
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT id_usuario AS IdUsuario, nombre AS Nombre, usuario AS Usuario, fecha_registro AS FechaRegistro FROM USUARIO WHERE id_usuario = @Id",
                new { Id = id }
            );
        }
        public async Task<Usuario?> LoginAsync(string usuario, string contrasena)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                @"SELECT id_usuario AS IdUsuario, nombre AS Nombre, usuario AS Usuario, fecha_registro AS FechaRegistro 
                  FROM USUARIO WHERE usuario = @usuario AND contrasena = @contrasena",
                new { usuario, contrasena }
            );
        }

    }
}