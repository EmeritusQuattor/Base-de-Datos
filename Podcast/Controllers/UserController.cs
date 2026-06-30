using System.Collections.Generic;
using System.Threading.Tasks;
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
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<UserC> users = await this._repo.GetAllAsync();
            return this.Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserC? user = await this._repo.GetByIdAsync(id);
            if (user == null) return this.NotFound();
            return this.Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserC user)
        {
            int id = await this._repo.InsertAsync(user);
            return this.Ok(new { id_user_new = id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserC user)
        {
            UserC? result = await this._repo.LoginAsync(user.User, user.Password);
            if (result == null) return this.Unauthorized();
            return this.Ok(result);
        }
    }
}
