using Microsoft.AspNetCore.Mvc;
using Podcast.Models;
using Podcast.Repositories;

namespace Podcast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repo;
        public UserController(UserRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repo.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(UserC user)
        {
            var id = await _repo.InsertAsync(user);
            return Ok(new { id_user_new = id });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserC user)
        {
            var result = await _repo.LoginAsync(user.User, user.Password);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}