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
    public class RoleController : ControllerBase
    {
        private readonly DataContext _context;

        public RoleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            // Obtener todos los roles
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles); // Retornar la lista de roles
        }

        // GET: api/role/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            // Buscar un rol por ID
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el rol
            }
            return Ok(role); // Retornar el rol encontrado
        }

        // POST: api/role
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            // Crear un nuevo rol
            _context.Roles.Add(role);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role); // Retornar 201 Created
        }

        // PUT: api/role/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            // Actualizar un rol existente
            if (id != role.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(role).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound(); // Retornar 404 si el rol no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/role/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            // Eliminar un rol
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound(); // Retornar 404 si el rol no se encuentra
            }

            _context.Roles.Remove(role); // Eliminar el rol
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id); // Verificar si el rol existe
        }
    }
}
