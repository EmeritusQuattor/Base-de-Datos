using Microsoft.AspNetCore.Mvc;
using Podcast.Models;
using Podcast.Repositories;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PodcastController : ControllerBase
    {
        private readonly PodcastRepository _repo;

        public PodcastController(PodcastRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var podcasts = await _repo.GetAllAsync();
            return Ok(podcasts);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetByUsuario(int idUsuario)
        {
            var podcasts = await _repo.GetByUsuarioAsync(idUsuario);
            return Ok(podcasts);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(PodcastModel podcast)
        {
            var id = await _repo.InsertAsync(podcast);
            return Ok(new { id_podcast_nuevo = id });
        }
    }
}