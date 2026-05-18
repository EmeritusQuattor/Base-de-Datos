using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly string _connectionString;

        public CategoriaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            var categorias = await conn.QueryAsync<Category>(
                @"SELECT id_categoria AS IdCategoria, 
                  nombre AS Nombre, 
                  descripcion AS Descripcion 
                  FROM CATEGORIA"
            );
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Category categoria)
        {
            using var conn = new SqlConnection(_connectionString);
            var id = await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO CATEGORIA (nombre, descripcion) 
          VALUES (@nombre, @descripcion);
          SELECT SCOPE_IDENTITY();",
                new { nombre = categoria.Name, descripcion = categoria.Descripcion }
            );
            return Ok(new { id_categoria_nueva = id });
        }
    }
}