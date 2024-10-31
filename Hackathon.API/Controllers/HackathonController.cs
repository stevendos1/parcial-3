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
    public class HackathonController : ControllerBase
    {
        private readonly DataContext _context;

        public HackathonController(DataContext context)
        {
            _context = context;
        }

        // GET: api/hackathon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hackathon>>> GetHackathons()
        {
            // Obtener todos los hackatones
            var hackathons = await _context.Hackathons.ToListAsync();
            return Ok(hackathons);
        }

        // GET: api/hackathon/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Hackathon>> GetHackathon(int id)
        {
            // Buscar un hackathon por ID
            var hackathon = await _context.Hackathons.FindAsync(id);
            if (hackathon == null)
            {
                return NotFound();
            }
            return Ok(hackathon);
        }

        // POST: api/hackathon
        [HttpPost]
        public async Task<ActionResult<Hackathon>> PostHackathon(Hackathon hackathon)
        {
            // Crear un nuevo hackathon
            _context.Hackathons.Add(hackathon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHackathon), new { id = hackathon.Id }, hackathon);
        }

        // PUT: api/hackathon/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHackathon(int id, Hackathon hackathon)
        {
            // Actualizar un hackathon existente
            if (id != hackathon.Id)
            {
                return BadRequest();
            }

            _context.Entry(hackathon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HackathonExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/hackathon/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHackathon(int id)
        {
            // Eliminar un hackathon
            var hackathon = await _context.Hackathons.FindAsync(id);
            if (hackathon == null)
            {
                return NotFound();
            }

            _context.Hackathons.Remove(hackathon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HackathonExists(int id)
        {
            return _context.Hackathons.Any(e => e.Id == id);
        }
    }
}
