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
    public class EvaluationController : ControllerBase
    {
        private readonly DataContext _context;

        public EvaluationController(DataContext context)
        {
            _context = context;
        }

        // GET: api/evaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluation>>> GetEvaluations()
        {
            // Obtener todas las evaluaciones
            var evaluations = await _context.Evaluations.ToListAsync();
            return Ok(evaluations);
        }

        // GET: api/evaluation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetEvaluation(int id)
        {
            // Buscar una evaluación por ID
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }
            return Ok(evaluation);
        }

        // POST: api/evaluation
        [HttpPost]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        {
            // Crear una nueva evaluación
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvaluation), new { id = evaluation.Id }, evaluation);
        }

        // PUT: api/evaluation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluation(int id, Evaluation evaluation)
        {
            // Actualizar una evaluación existente
            if (id != evaluation.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/evaluation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            // Eliminar una evaluación
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            _context.Evaluations.Remove(evaluation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluationExists(int id)
        {
            return _context.Evaluations.Any(e => e.Id == id);
        }
    }
}
