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
            _repo = repo;
        }
        [HttpGet("user/{idUser}")]
        public async Task<IActionResult> GetByUser(int idUser)
        {
            var reproductions = await _repo.GetByUserAsync(idUser);
            return Ok(reproductions);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(Reproduction reproduction)
        {
            var id = await _repo.InsertAsync(reproduction);
            return Ok(new { id_reproduction_new = id });
        }
    }
}