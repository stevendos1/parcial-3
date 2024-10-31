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
            // Obtener todos los criterios
            var criteria = await _context.Criteria.ToListAsync();
            return Ok(criteria);
        }

        // GET: api/criterion/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Criterion>> GetCriterion(int id)
        {
            // Buscar un criterio por ID
            var criterion = await _context.Criteria.FindAsync(id);
            if (criterion == null)
            {
                return NotFound();
            }
            return Ok(criterion);
        }

        // POST: api/criterion
        [HttpPost]
        public async Task<ActionResult<Criterion>> PostCriterion(Criterion criterion)
        {
            // Crear un nuevo criterio
            _context.Criteria.Add(criterion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCriterion), new { id = criterion.Id }, criterion);
        }

        // PUT: api/criterion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriterion(int id, Criterion criterion)
        {
            // Actualizar un criterio existente
            if (id != criterion.Id)
            {
                return BadRequest();
            }

            _context.Entry(criterion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriterionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/criterion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriterion(int id)
        {
            // Eliminar un criterio
            var criterion = await _context.Criteria.FindAsync(id);
            if (criterion == null)
            {
                return NotFound();
            }

            _context.Criteria.Remove(criterion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CriterionExists(int id)
        {
            return _context.Criteria.Any(e => e.Id == id);
        }
    }
}
