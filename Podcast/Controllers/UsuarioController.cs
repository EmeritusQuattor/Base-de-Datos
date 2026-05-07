using Microsoft.AspNetCore.Mvc;
using Podcast.Models;
using Podcast.Repositories;
using static Podcast.Repositories.UsuarioRepositories;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _repo;

        public UsuarioController(UsuarioRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _repo.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _repo.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Usuario usuario)
        {
            var id = await _repo.InsertAsync(usuario);
            return Ok(new { id_usuario_nuevo = id });
        }
    }
}