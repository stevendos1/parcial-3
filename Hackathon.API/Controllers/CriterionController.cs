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
    public class CriterionController : ControllerBase
    {
        private readonly DataContext _context;

        public CriterionController(DataContext context)
        {
            _context = context;
        }

        // GET: api/criterion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criterion>>> GetCriteria()
        {
            var criteria = await _context.Criteria.ToListAsync(); // Obtener todos los criterios
            return Ok(criteria);
        }

        // GET: api/criterion/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Criterion>> GetCriterion(int id)
        {
            var criterion = await _context.Criteria.FindAsync(id); // Buscar un criterio por ID
            if (criterion == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra el criterio
            }
            return Ok(criterion); // Retornar el criterio encontrado
        }

        // POST: api/criterion
        [HttpPost]
        public async Task<ActionResult<Criterion>> PostCriterion(Criterion criterion)
        {
            _context.Criteria.Add(criterion); // Crear un nuevo criterio
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return CreatedAtAction(nameof(GetCriterion), new { id = criterion.Id }, criterion); // Retornar 201 Created
        }

        // PUT: api/criterion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriterion(int id, Criterion criterion)
        {
            if (id != criterion.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(criterion).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriterionExists(id))
                {
                    return NotFound(); // Retornar 404 si el criterio no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/criterion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriterion(int id)
        {
            var criterion = await _context.Criteria.FindAsync(id); // Eliminar un criterio
            if (criterion == null)
            {
                return NotFound(); // Retornar 404 si el criterio no se encuentra
            }

            _context.Criteria.Remove(criterion); // Eliminar el criterio
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool CriterionExists(int id)
        {
            return _context.Criteria.Any(e => e.Id == id); // Verificar si el criterio existe
        }
    }
}
