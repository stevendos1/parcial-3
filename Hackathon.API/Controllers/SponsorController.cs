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
    public class SponsorController : ControllerBase
    {
        private readonly DataContext _context;

        public SponsorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/sponsor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sponsor>>> GetSponsors()
        {
            // Obtener todos los patrocinadores
            var sponsors = await _context.Sponsors.ToListAsync();
            return Ok(sponsors);
        }

        // GET: api/sponsor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sponsor>> GetSponsor(int id)
        {
            // Buscar un patrocinador por ID
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return Ok(sponsor);
        }

        // POST: api/sponsor
        [HttpPost]
        public async Task<ActionResult<Sponsor>> PostSponsor(Sponsor sponsor)
        {
            // Crear un nuevo patrocinador
            _context.Sponsors.Add(sponsor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSponsor), new { id = sponsor.Id }, sponsor);
        }

        // PUT: api/sponsor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSponsor(int id, Sponsor sponsor)
        {
            // Actualizar un patrocinador existente
            if (id != sponsor.Id)
            {
                return BadRequest();
            }

            _context.Entry(sponsor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/sponsor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsor(int id)
        {
            // Eliminar un patrocinador
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            _context.Sponsors.Remove(sponsor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SponsorExists(int id)
        {
            return _context.Sponsors.Any(e => e.Id == id);
        }
    }
}
