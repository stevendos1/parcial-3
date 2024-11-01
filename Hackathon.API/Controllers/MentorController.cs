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
    public class MentorController : ControllerBase
    {
        private readonly DataContext _context;

        public MentorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/mentor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mentor>>> GetMentors()
        {
            // Obtener todos los mentores
            var mentors = await _context.Mentors.ToListAsync();
            return Ok(mentors); // Retornar la lista de mentores
        }

        // GET: api/mentor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Mentor>> GetMentor(int id)
        {
            // Buscar un mentor por ID
            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el mentor
            }
            return Ok(mentor); // Retornar el mentor encontrado
        }

        // POST: api/mentor
        [HttpPost]
        public async Task<ActionResult<Mentor>> PostMentor(Mentor mentor)
        {
            // Crear un nuevo mentor
            _context.Mentors.Add(mentor);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetMentor), new { id = mentor.Id }, mentor); // Retornar 201 Created
        }

        // PUT: api/mentor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentor(int id, Mentor mentor)
        {
            // Actualizar un mentor existente
            if (id != mentor.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(mentor).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentorExists(id))
                {
                    return NotFound(); // Retornar 404 si el mentor no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/mentor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            // Eliminar un mentor
            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return NotFound(); // Retornar 404 si el mentor no se encuentra
            }

            _context.Mentors.Remove(mentor); // Eliminar el mentor
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool MentorExists(int id)
        {
            return _context.Mentors.Any(e => e.Id == id); // Verificar si el mentor existe
        }
    }
}
