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
    public class ScheduleController : ControllerBase
    {
        private readonly DataContext _context;

        public ScheduleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/schedule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            // Obtener todos los horarios
            var schedules = await _context.Schedules.ToListAsync();
            return Ok(schedules); // Retornar la lista de horarios
        }

        // GET: api/schedule/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            // Buscar un horario por ID
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el horario
            }
            return Ok(schedule); // Retornar el horario encontrado
        }

        // POST: api/schedule
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            // Crear un nuevo horario
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, schedule); // Retornar 201 Created
        }

        // PUT: api/schedule/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            // Actualizar un horario existente
            if (id != schedule.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(schedule).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound(); // Retornar 404 si el horario no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/schedule/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            // Eliminar un horario
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound(); // Retornar 404 si el horario no se encuentra
            }

            _context.Schedules.Remove(schedule); // Eliminar el horario
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id); // Verificar si el horario existe
        }
    }
}
