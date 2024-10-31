using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hackathon.API.Data;
using Hackathon.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwardController : ControllerBase
    {
        private readonly DataContext _context;

        public AwardController(DataContext context)
        {
            _context = context;
        }

        // GET: api/award
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Award>>> GetAwards()
        {
            // Obtener todos los premios
            var awards = await _context.Awards.ToListAsync();
            return Ok(awards);
        }

        // GET: api/award/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Award>> GetAward(int id)
        {
            // Buscar un premio por ID
            var award = await _context.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return Ok(award);
        }

        // POST: api/award
        [HttpPost]
        public async Task<ActionResult<Award>> PostAward(Award award)
        {
            // Crear un nuevo premio
            _context.Awards.Add(award);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAward), new { id = award.Id }, award);
        }

        // PUT: api/award/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAward(int id, Award award)
        {
            // Actualizar un premio existente
            if (id != award.Id)
            {
                return BadRequest();
            }

            _context.Entry(award).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AwardExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/award/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            // Eliminar un premio
            var award = await _context.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound();
            }

            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AwardExists(int id)
        {
            return _context.Awards.Any(e => e.Id == id);
        }
    }
}
