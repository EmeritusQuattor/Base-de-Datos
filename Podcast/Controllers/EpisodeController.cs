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
            this._repo = repo;
        }
        [HttpGet("podcast/{idPodcast}")]
        public async Task<IActionResult> GetByPodcast(int idPodcast)
        {
            IEnumerable<Episode> episodes = await this._repo.GetByPodcastAsync(idPodcast);
            return this.Ok(episodes);
        }
        [HttpGet("audio/{idEpisode}")]
        public async Task<IActionResult> GetAudio(int idEpisode)
        {
            Episode? episode = await this._repo.GetAudioAsync(idEpisode);
            if (episode == null) return this.NotFound();
            return this.File(episode.AudioData!, "audio/mpeg");
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? keyword,
            [FromQuery] string? category)
        {
            IEnumerable<Episode> results = await this._repo.SearchAsync(keyword, category);
            return this.Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Episode episode, IFormFile? audioFile)
        {
            if (audioFile != null)
            {
                using MemoryStream ms = new MemoryStream();
                await audioFile.CopyToAsync(ms);
                episode.AudioData = ms.ToArray();
            }
            int id = await this._repo.InsertAsync(episode);
            return this.Ok(new { id_episode_new = id });
        }
    }
}
