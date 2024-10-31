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
    public class TeamController : ControllerBase
    {
        private readonly DataContext _context;

        public TeamController(DataContext context)
        {
            _context = context;
        }

        // GET: api/team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            // Obtener todos los equipos
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams); // Retornar la lista de equipos
        }

        // GET: api/team/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            // Buscar un equipo por ID
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound(); // Retornar 404 si el equipo no se encuentra
            }
            return Ok(team); // Retornar el equipo encontrado
        }

        // POST: api/team
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            // Crear un nuevo equipo
            _context.Teams.Add(team);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team); // Retornar 201 Created
        }

        // PUT: api/team/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            // Actualizar un equipo existente
            if (id != team.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(team).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound(); // Retornar 404 si el equipo no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/team/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            // Eliminar un equipo
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound(); // Retornar 404 si el equipo no se encuentra
            }

            _context.Teams.Remove(team); // Eliminar el equipo
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id); // Verificar si el equipo existe
        }
    }
}
