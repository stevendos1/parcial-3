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
			return Ok(participants);
		}

		// GET: api/participant/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Participant>> GetParticipant(int id)
		{
			// Buscar un participante por ID
			var participant = await _context.Participants.FindAsync(id);
			if (participant == null)
			{
				return NotFound();
			}
			return Ok(participant);
		}

		// POST: api/participant
		[HttpPost]
		public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
		{
			// Crear un nuevo participante
			_context.Participants.Add(participant);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
		}

		// PUT: api/participant/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> PutParticipant(int id, Participant participant)
		{
			// Actualizar un participante existente
			if (id != participant.Id)
			{
				return BadRequest();
			}

			_context.Entry(participant).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ParticipantExists(id))
				{
					return NotFound();
				}
				throw;
			}

			return NoContent();
		}

		// DELETE: api/participant/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteParticipant(int id)
		{
			// Eliminar un participante
			var participant = await _context.Participants.FindAsync(id);
			if (participant == null)
			{
				return NotFound();
			}

			_context.Participants.Remove(participant);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ParticipantExists(int id)
		{
			return _context.Participants.Any(e => e.Id == id);
		}
	}
}
