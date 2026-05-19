using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using Podcast.Models;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly string _connectionString;
        public CategoryController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            var categories = await conn.QueryAsync<Category>(
                @"SELECT id_categoria AS IdCategory,
                  nombre AS Name,
                  descripcion AS Description
                  FROM CATEGORIA"
            );
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(Category category)
        {
            using var conn = new SqlConnection(_connectionString);
            var id = await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO CATEGORIA (nombre, descripcion)
                  VALUES (@nombre, @descripcion);
                  SELECT SCOPE_IDENTITY();",
                new { nombre = category.Name, descripcion = category.Description }
            );
            return Ok(new { id_category_new = id });
        }
    }
}