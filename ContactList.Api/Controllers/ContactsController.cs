using ContactList.Api.Data;
using ContactList.Api.Model;
using ContactList.Shared.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactDbContext _context;

        public ContactsController(ContactDbContext context)
        {
            _context = context;
        }

        // GET: api/<ContactController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> Get()
        {
            var contacts = await _context.Contacts
                .Select(c => new ContactDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Password = c.Password,
                    PhoneNumber = c.PhoneNumber,
                    DateOfBirth = c.DateOfBirth,
                    Category = c.Category,
                    SubcategoryId = c.Subcategory != null ? c.Subcategory.Id : null,
                    SubcategoryName = c.Subcategory != null ? c.Subcategory.Name : null
                })
                .ToListAsync();

            return Ok(contacts);
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            var dto = new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Password = contact.Password,
                PhoneNumber = contact.PhoneNumber,
                DateOfBirth = contact.DateOfBirth,
                Category = contact.Category,
                SubcategoryId = contact.Subcategory?.Id,
                SubcategoryName = contact.Subcategory?.Name
            };
            return Ok(dto);
        }

        // POST api/<ContactController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] ContactDto dto)
        {
            var contact = new Contact
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Category = dto.Category,
                SubcategoryId = await ResolveSubcategoryIdAsync(dto)
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            var result = new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Password = contact.Password,
                PhoneNumber = contact.PhoneNumber,
                DateOfBirth = contact.DateOfBirth,
                Category = contact.Category,
                SubcategoryId = contact.Subcategory?.Id,
                SubcategoryName = contact.Subcategory?.Name
            };
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, result);
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, [FromBody] ContactDto dto)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            var oldSubcategoryId = contact.SubcategoryId;

            contact.FirstName = dto.FirstName;
            contact.LastName = dto.LastName;
            contact.Email = dto.Email;
            contact.Password = dto.Password;
            contact.PhoneNumber = dto.PhoneNumber;
            contact.DateOfBirth = dto.DateOfBirth;
            contact.Category = dto.Category;
            contact.SubcategoryId = await ResolveSubcategoryIdAsync(dto);
            await _context.SaveChangesAsync();

            await HandleOldSubcategory(oldSubcategoryId, contact.SubcategoryId);
            return NoContent();
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await HandleOldSubcategory(contact.SubcategoryId, null);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<int?> ResolveSubcategoryIdAsync(ContactDto dto)
        {
            switch (dto.Category)
            {
                case Category.Private:
                    return null;
                case Category.Professional:
                    return dto.SubcategoryId;
                case Category.Other:
                    // Create a new subcategory if one with this name does not exist
                    Subcategory? sub = await _context.Subcategories
                        .FirstOrDefaultAsync(s => s.Category == Category.Other && s.Name == dto.SubcategoryName);
                    if (sub == null)
                    {
                        sub = new Subcategory
                        {
                            Name = dto.SubcategoryName!,
                            Category = Category.Other
                        };
                        _context.Subcategories.Add(sub);
                        await _context.SaveChangesAsync();
                    }
                    return sub.Id;
                default:
                    return null;
            }
        }

        private async Task HandleOldSubcategory(int? oldSubcategoryId, int? newSubcategoryId)
        {
            if (oldSubcategoryId == null || oldSubcategoryId == newSubcategoryId)
                return;

            // Delete old, orphaned subcategories.
            bool stillUsed = await _context.Contacts.AnyAsync(c => c.SubcategoryId == oldSubcategoryId);
            if (!stillUsed)
            {
                var sub = await _context.Subcategories.FindAsync(oldSubcategoryId);
                if (sub != null && sub.Category == Category.Other)
                {
                    _context.Subcategories.Remove(sub);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}
