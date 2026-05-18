using Microsoft.AspNetCore.Mvc;
using Podcast.Models;
using Podcast.Repositories;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodioController : ControllerBase
    {
        private readonly EpisodeRepository _repo;

        public EpisodioController(EpisodeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("podcast/{idPodcast}")]
        public async Task<IActionResult> GetByPodcast(int idPodcast)
        {
            var episodios = await _repo.GetByPodcastAsync(idPodcast);
            return Ok(episodios);
        }

        [HttpGet("audio/{idEpisodio}")]
        public async Task<IActionResult> GetAudio(int idEpisodio)
        {
            var episodio = await _repo.GetAudioAsync(idEpisodio);
            if (episodio == null) return NotFound();
            return File(episodio.AudioData!, "audio/mpeg");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? keyword,
            [FromQuery] string? categoria)
        {
            var resultados = await _repo.SearchAsync(keyword, categoria);
            return Ok(resultados);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Episode episodio, IFormFile? audioFile)
        {
            if (audioFile != null)
            {
                using var ms = new MemoryStream();
                await audioFile.CopyToAsync(ms);
                episodio.AudioData = ms.ToArray();
            }
            var id = await _repo.InsertAsync(episodio);
            return Ok(new { id_episodio_nuevo = id });
        }
    }
}