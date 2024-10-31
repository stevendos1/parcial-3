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
    public class ParticipantController : ControllerBase
    {
        private readonly DataContext _context;

        public ParticipantController(DataContext context)
        {
            _context = context;
        }

        // GET: api/participant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
        {
            // Obtener todos los participantes
            var participants = await _context.Participants.ToListAsync();
            return Ok(participants); // Retornar la lista de participantes
        }

        // GET: api/participant/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            // Buscar un participante por ID
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el participante
            }
            return Ok(participant); // Retornar el participante encontrado
        }

        // POST: api/participant
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
            // Crear un nuevo participante
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant); // Retornar 201 Created
        }

        // PUT: api/participant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, Participant participant)
        {
            // Actualizar un participante existente
            if (id != participant.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(participant).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                {
                    return NotFound(); // Retornar 404 si el participante no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/participant/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            // Eliminar un participante
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound(); // Retornar 404 si el participante no se encuentra
            }

            _context.Participants.Remove(participant); // Eliminar el participante
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participants.Any(e => e.Id == id); // Verificar si el participante existe
        }
    }
}
