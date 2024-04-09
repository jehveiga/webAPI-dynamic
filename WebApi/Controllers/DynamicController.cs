using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController<T> : ControllerBase where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public DynamicController(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll() => await _dbSet.ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<T>> GetById([FromRoute] int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null)
                return NotFound();

            return entity;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.GetType().GetProperty("Id").GetValue(entity) }, entity);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] T entity, [FromRoute] int id)
        {
            if (id != (int)entity.GetType().GetProperty("Id").GetValue(entity))
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null)
                return NotFound();

            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return NoContent();
        }




    }
}
