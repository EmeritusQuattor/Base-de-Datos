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
            this._repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<PodcastModel> podcasts = await this._repo.GetAllAsync();
            return this.Ok(podcasts);
        }
        [HttpGet("user/{idUser}")]
        public async Task<IActionResult> GetByUser(int idUser)
        {
            IEnumerable<PodcastModel> podcasts = await this._repo.GetByUserAsync(idUser);
            return this.Ok(podcasts);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(PodcastModel podcast)
        {
            int id = await this._repo.InsertAsync(podcast);
            return this.Ok(new { id_podcast_new = id });
        }
    }
}
