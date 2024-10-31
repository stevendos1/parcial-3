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
            return Ok(mentors);
        }

        // GET: api/mentor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Mentor>> GetMentor(int id)
        {
            // Buscar un mentor por ID
            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return NotFound();
            }
            return Ok(mentor);
        }

        // POST: api/mentor
        [HttpPost]
        public async Task<ActionResult<Mentor>> PostMentor(Mentor mentor)
        {
            // Crear un nuevo mentor
            _context.Mentors.Add(mentor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMentor), new { id = mentor.Id }, mentor);
        }

        // PUT: api/mentor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentor(int id, Mentor mentor)
        {
            // Actualizar un mentor existente
            if (id != mentor.Id)
            {
                return BadRequest();
            }

            _context.Entry(mentor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentorExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/mentor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            // Eliminar un mentor
            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return NotFound();
            }

            _context.Mentors.Remove(mentor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MentorExists(int id)
        {
            return _context.Mentors.Any(e => e.Id == id);
        }
    }
}
