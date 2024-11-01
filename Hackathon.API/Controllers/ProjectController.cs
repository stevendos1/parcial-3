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
    public class ProjectController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectController(DataContext context)
        {
            _context = context;
        }

        // GET: api/project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            // Obtener todos los proyectos
            var projects = await _context.Projects.ToListAsync();
            return Ok(projects); // Retornar la lista de proyectos
        }

        // GET: api/project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            // Buscar un proyecto por ID
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el proyecto
            }
            return Ok(project); // Retornar el proyecto encontrado
        }

        // POST: api/project
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            // Crear un nuevo proyecto
            _context.Projects.Add(project);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project); // Retornar 201 Created
        }

        // PUT: api/project/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            // Actualizar un proyecto existente
            if (id != project.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(project).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound(); // Retornar 404 si el proyecto no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            // Eliminar un proyecto
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound(); // Retornar 404 si el proyecto no se encuentra
            }

            _context.Projects.Remove(project); // Eliminar el proyecto
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id); // Verificar si el proyecto existe
        }
    }
}
