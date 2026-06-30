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
            this._connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            IEnumerable<Category> categories = await conn.QueryAsync<Category>(
                @"SELECT id_categoria AS IdCategory,
                  nombre AS Name,
                  descripcion AS Description
                  FROM CATEGORIA"
            );
            return this.Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(Category category)
        {
            using SqlConnection conn = new SqlConnection(this._connectionString);
            int id = await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO CATEGORIA (nombre, descripcion)
                  VALUES (@nombre, @descripcion);
                  SELECT SCOPE_IDENTITY();",
                new { nombre = category.Name, descripcion = category.Description }
            );
            return this.Ok(new { id_category_new = id });
        }
    }
}