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
                return NotFound(); // Retornar 404 si no se encuentra el premio
            }
            return Ok(award); // Retornar el premio encontrado
        }

        // POST: api/award
        [HttpPost]
        public async Task<ActionResult<Award>> PostAward(Award award)
        {
            // Crear un nuevo premio
            _context.Awards.Add(award);
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return CreatedAtAction(nameof(GetAward), new { id = award.Id }, award); // Retornar 201 Created
        }

        // PUT: api/award/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAward(int id, Award award)
        {
            // Actualizar un premio existente
            if (id != award.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(award).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AwardExists(id))
                {
                    return NotFound(); // Retornar 404 si el premio no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/award/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            // Eliminar un premio
            var award = await _context.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound(); // Retornar 404 si el premio no se encuentra
            }

            _context.Awards.Remove(award); // Eliminar el premio
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool AwardExists(int id)
        {
            return _context.Awards.Any(e => e.Id == id); // Verificar si el premio existe
        }
    }
}
