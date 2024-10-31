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
            return Ok(sponsors); // Retornar la lista de patrocinadores
        }

        // GET: api/sponsor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sponsor>> GetSponsor(int id)
        {
            // Buscar un patrocinador por ID
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el patrocinador
            }
            return Ok(sponsor); // Retornar el patrocinador encontrado
        }

        // POST: api/sponsor
        [HttpPost]
        public async Task<ActionResult<Sponsor>> PostSponsor(Sponsor sponsor)
        {
            // Crear un nuevo patrocinador
            _context.Sponsors.Add(sponsor);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetSponsor), new { id = sponsor.Id }, sponsor); // Retornar 201 Created
        }

        // PUT: api/sponsor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSponsor(int id, Sponsor sponsor)
        {
            // Actualizar un patrocinador existente
            if (id != sponsor.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(sponsor).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorExists(id))
                {
                    return NotFound(); // Retornar 404 si el patrocinador no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/sponsor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsor(int id)
        {
            // Eliminar un patrocinador
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound(); // Retornar 404 si el patrocinador no se encuentra
            }

            _context.Sponsors.Remove(sponsor); // Eliminar el patrocinador
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool SponsorExists(int id)
        {
            return _context.Sponsors.Any(e => e.Id == id); // Verificar si el patrocinador existe
        }
    }
}
