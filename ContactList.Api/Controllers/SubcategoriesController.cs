using ContactList.Api.Data;
using ContactList.Api.Model;
using ContactList.Shared.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ContactDbContext _context;

        public SubcategoriesController(ContactDbContext context)
        {
            _context = context;
        }

        // GET: api/<SubcategoriesController>?category=Professional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubcategoryDto>>> GetByCategory([FromQuery] Category? category)
        {
            var subcategories = await _context.Subcategories
                .Where(s => category == null || s.Category == category)
                .Select(s => new SubcategoryDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category
                })
                .ToListAsync();
            return Ok(subcategories);
        }

        // GET api/<SubcategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubcategoryDto>> Get(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            var dto = new SubcategoryDto
            {
                Id = subcategory.Id,
                Name = subcategory.Name,
                Category = subcategory.Category
            };
            return Ok(dto);
        }
    }
}
