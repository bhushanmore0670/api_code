using ContactsApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<List<Contact>> Get() => _contactService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            var contact = _contactService.Get(id);
            if (contact == null) return NotFound();
            return contact;
        }

        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            _contactService.Add(contact);
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Contact contact)
        {
            if (id != contact.Id) return BadRequest();
            var existingContact = _contactService.Get(id);
            if (existingContact == null) return NotFound();

            _contactService.Update(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _contactService.Get(id);
            if (contact == null) return NotFound();

            _contactService.Delete(id);
            return NoContent();
        }
    }
}
