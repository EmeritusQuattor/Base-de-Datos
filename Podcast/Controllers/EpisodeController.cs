using Microsoft.AspNetCore.Mvc;
using Podcast.Models;
using Podcast.Repositories;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodeController : ControllerBase
    {
        private readonly EpisodeRepository _repo;
        public EpisodeController(EpisodeRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("podcast/{idPodcast}")]
        public async Task<IActionResult> GetByPodcast(int idPodcast)
        {
            var episodes = await _repo.GetByPodcastAsync(idPodcast);
            return Ok(episodes);
        }
        [HttpGet("audio/{idEpisode}")]
        public async Task<IActionResult> GetAudio(int idEpisode)
        {
            var episode = await _repo.GetAudioAsync(idEpisode);
            if (episode == null) return NotFound();
            return File(episode.AudioData!, "audio/mpeg");
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? keyword,
            [FromQuery] string? category)
        {
            var results = await _repo.SearchAsync(keyword, category);
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Episode episode, IFormFile? audioFile)
        {
            if (audioFile != null)
            {
                using var ms = new MemoryStream();
                await audioFile.CopyToAsync(ms);
                episode.AudioData = ms.ToArray();
            }
            var id = await _repo.InsertAsync(episode);
            return Ok(new { id_episode_new = id });
        }
    }
}