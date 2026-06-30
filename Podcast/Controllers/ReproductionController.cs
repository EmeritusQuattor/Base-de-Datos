using Microsoft.AspNetCore.Mvc;
using Podcast.Models;
using Podcast.Repositories;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReproductionController : ControllerBase
    {
        private readonly ReproductionRepository _repo;
        public ReproductionController(ReproductionRepository repo)
        {
            this._repo = repo;
        }
        [HttpGet("user/{idUser}")]
        public async Task<IActionResult> GetByUser(int idUser)
        {
            IEnumerable<Reproduction> reproductions = await this._repo.GetByUserAsync(idUser);
            return this.Ok(reproductions);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(Reproduction reproduction)
        {
            int id = await this._repo.InsertAsync(reproduction);
            return this.Ok(new { id_reproduction_new = id });
        }
    }
}
