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
            return Ok(teams);
        }

        // GET: api/team/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            // Buscar un equipo por ID
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        // POST: api/team
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            // Crear un nuevo equipo
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        // PUT: api/team/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            // Actualizar un equipo existente
            if (id != team.Id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/team/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            // Eliminar un equipo
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
