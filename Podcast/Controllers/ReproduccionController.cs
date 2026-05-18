using Microsoft.AspNetCore.Mvc;
using Podcast.Models;
using Podcast.Repositories;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReproduccionController : ControllerBase
    {
        private readonly ReproductionRepository _repo;

        public ReproduccionController(ReproductionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetByUsuario(int idUsuario)
        {
            var reproducciones = await _repo.GetByUsuarioAsync(idUsuario);
            return Ok(reproducciones);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Reproduction reproduccion)
        {
            var id = await _repo.InsertAsync(reproduccion);
            return Ok(new { id_reproduccion_nueva = id });
        }
    }
}