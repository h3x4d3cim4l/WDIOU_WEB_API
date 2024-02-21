using Microsoft.AspNetCore.Mvc;
using WDIOU_WEB_API.Models;
using WDIOU_WEB_API.Services;

namespace WDIOU_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }


        [HttpGet("{ownr_username}")]
        public async Task<ActionResult<List<Person>>> Get(string ownr_username)
        {
            var personList = await _personService.GetPersonsOfUser(ownr_username);
            
            if (personList is null)
            {
                return NotFound();
            }

            return Ok(personList);
        }

        [HttpGet("{ownr_username}/{pname}")]
        public async Task<ActionResult<Person>> Get(string ownr_username, string pname)
        {
            var person = await _personService.GetPersonOfUser(ownr_username, pname);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            await _personService.CreatePerson(person);

            return CreatedAtAction(nameof(Post), new {id=person.Id}, person);
        }


        [HttpPut("{ownr_username}/{pname}")]
        public async Task<IActionResult> Update(string ownr_username, string pname, Person newPerson)
        {
            var person = await _personService.GetPersonOfUser(ownr_username,pname);

            if(person is null)
            {
                return NotFound();
            }
            await _personService.UpdatePerson(ownr_username, pname, newPerson);
            return NoContent();

        }

        [HttpDelete("{ownr_username}/{pname}")]
        public async Task<IActionResult> Delete(string ownr_username, string pname)
        {
            var person = await _personService.GetPersonOfUser(ownr_username, pname);

            if(person is null)
            {
                return NotFound();
            }

            await _personService.DeletePerson(ownr_username, pname);
            return NoContent();
        }



    }
}
