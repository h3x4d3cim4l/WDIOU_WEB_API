using Microsoft.AspNetCore.Mvc;
using WDIOU_WEB_API.Models;
using WDIOU_WEB_API.Services;

namespace WDIOU_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class usedEmailsController : ControllerBase
    {
        private readonly usedEmailsService _usedEmailsService;

        public usedEmailsController(usedEmailsService usedEmailsService)
        {
            _usedEmailsService = usedEmailsService;
        }

        [HttpGet]
        public async Task<List<usedEmail>> Get() =>
            await _usedEmailsService.GetUsedEmails();


        [HttpGet("{email}")]
        public async Task<ActionResult<usedEmail>> Get(string email)
        {
            var usedEmail = await _usedEmailsService.GetUsedEmail(email);

            if (usedEmail is null)
            {
                return NotFound();
            }

            return Ok(usedEmail);
        }

        [HttpPost]
        public async Task<IActionResult> Post(usedEmail email)
        {
            await _usedEmailsService.CreateUsedEmail(email);

            return CreatedAtAction(nameof(Get), new {id = email.Id} , email);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, usedEmail newUsedEmail)
        {
            var usedEmail = await _usedEmailsService.GetUsedEmail(email);

            if (usedEmail is null)
            {
                return NotFound();
            }

            await _usedEmailsService.UpdateUsedEmail(email, newUsedEmail);
            return NoContent();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var usedEmail = await _usedEmailsService.GetUsedEmail(email);

            if( usedEmail is null )
            {
                return NotFound();
            }

            await _usedEmailsService.DeleteUsedEmail(email);
            return NoContent();
        }

    }
}
