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
    public class OrganizationController : ControllerBase
    {
        private readonly DataContext _context;

        public OrganizationController(DataContext context)
        {
            _context = context;
        }

        // GET: api/organization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            // Obtener todas las organizaciones
            var organizations = await _context.Organizations.ToListAsync();
            return Ok(organizations); // Retornar la lista de organizaciones
        }

        // GET: api/organization/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            // Buscar una organización por ID
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra la organización
            }
            return Ok(organization); // Retornar la organización encontrada
        }

        // POST: api/organization
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            // Crear una nueva organización
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return CreatedAtAction(nameof(GetOrganization), new { id = organization.Id }, organization); // Retornar 201 Created
        }

        // PUT: api/organization/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(int id, Organization organization)
        {
            // Actualizar una organización existente
            if (id != organization.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(organization).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
                {
                    return NotFound(); // Retornar 404 si la organización no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/organization/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            // Eliminar una organización
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound(); // Retornar 404 si la organización no se encuentra
            }

            _context.Organizations.Remove(organization); // Eliminar la organización
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool OrganizationExists(int id)
        {
            return _context.Organizations.Any(e => e.Id == id); // Verificar si la organización existe
        }
    }
}
