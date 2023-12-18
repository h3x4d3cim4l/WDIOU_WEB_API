using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WDIOU_WEB_API.Models;
using WDIOU_WEB_API.Services;

namespace WDIOU_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService) =>
            _usersService = usersService;

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _usersService.GetUsersAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _usersService.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _usersService.CreateUserAsync(newUser);

            return CreatedAtAction(nameof(Get), new {id = newUser.Id}, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            var user = await _usersService.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _usersService.UpdateUserAsync(id, updatedUser);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _usersService.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _usersService.RemoveUserAsync(id);
            return NoContent();
        }
    }
}
