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
        public async Task<ActionResult<IEnumerable<HackathonEntity>>> GetHackathons()
        {
            // Obtener todos los hackatones
            var hackathons = await _context.HackathonEntities.ToListAsync();
            return Ok(hackathons);
        }

        // GET: api/hackathon/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HackathonEntity>> GetHackathon(int id)
        {
            // Buscar un hackathon por ID
            var hackathon = await _context.HackathonEntities.FindAsync(id); 
            if (hackathon == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el hackathon
            }
            return Ok(hackathon); // Retornar el hackathon encontrado
        }

        // POST: api/hackathon
        [HttpPost]
        public async Task<ActionResult<HackathonEntity>> PostHackathon(HackathonEntity hackathon)
        {
            // Crear un nuevo hackathon
            _context.HackathonEntities.Add(hackathon); 
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return CreatedAtAction(nameof(GetHackathon), new { id = hackathon.Id }, hackathon); // Retornar 201 Created
        }

        // PUT: api/hackathon/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHackathon(int id, HackathonEntity hackathon)
        {
            // Actualizar un hackathon existente
            if (id != hackathon.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(hackathon).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HackathonExists(id))
                {
                    return NotFound(); // Retornar 404 si el hackathon no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/hackathon/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHackathon(int id)
        {
            // Eliminar un hackathon
            var hackathon = await _context.HackathonEntities.FindAsync(id); 
            if (hackathon == null)
            {
                return NotFound(); // Retornar 404 si el hackathon no se encuentra
            }

            _context.HackathonEntities.Remove(hackathon); 
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool HackathonExists(int id)
        {
            return _context.HackathonEntities.Any(e => e.Id == id); 
        }
    }
}
