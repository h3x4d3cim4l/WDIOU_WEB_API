using Microsoft.AspNetCore.Mvc;
using WDIOU_WEB_API.Models;
using WDIOU_WEB_API.Services;

namespace WDIOU_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtController : ControllerBase
    {
        private readonly DebtService _debtService;

        public DebtController(DebtService debtService)
        {
            _debtService = debtService;
        }


        [HttpGet("{ownr_username}")]
        public async Task<ActionResult<List<Debt>>> Get(string ownr_username)
        {
            var debtList = await _debtService.GetDebtsOfUser(ownr_username);
            if (debtList is null)
            {
                return NotFound();
            }

            return Ok(debtList);
        }

        [HttpGet("{ownr_username}/{id}")]
        public async Task<ActionResult<Debt>> Get(string ownr_username, string id)
        {
            var debt = await _debtService.GetDebtOfUser(ownr_username, id);

            if (debt is null)
            {
                return NotFound();
            }

            return Ok(debt);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Debt debt)
        {
            await _debtService.CreateDebt(debt);

            return CreatedAtAction(nameof(Post), new { id = debt.Id }, debt);
        }


        [HttpPut("{ownr_username}/{id}")]
        public async Task<IActionResult> Update(string ownr_username, string id, Debt newDebt)
        {
            var debt = await _debtService.GetDebtOfUser(ownr_username, id);

            if (debt is null)
            {
                return NotFound();
            }
            await _debtService.UpdateDebt(ownr_username, id, newDebt);
            return NoContent();

        }

        [HttpDelete("{ownr_username}/{id}")]
        public async Task<IActionResult> Delete(string ownr_username, string id)
        {
            var debt = await _debtService.GetDebtOfUser(ownr_username, id);

            if (debt is null)
            {
                return NotFound();
            }

            await _debtService.DeleteDebt(ownr_username, id);
            return NoContent();
        }



    }
}