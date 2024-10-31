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
            return Ok(evaluations); // Retornar la lista de evaluaciones
        }

        // GET: api/evaluation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetEvaluation(int id)
        {
            // Buscar una evaluación por ID
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra la evaluación
            }
            return Ok(evaluation); // Retornar la evaluación encontrada
        }

        // POST: api/evaluation
        [HttpPost]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        {
            // Crear una nueva evaluación
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return CreatedAtAction(nameof(GetEvaluation), new { id = evaluation.Id }, evaluation); // Retornar 201 Created
        }

        // PUT: api/evaluation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluation(int id, Evaluation evaluation)
        {
            // Actualizar una evaluación existente
            if (id != evaluation.Id)
            {
                return BadRequest(); // Retornar 400 si el ID no coincide
            }

            _context.Entry(evaluation).State = EntityState.Modified; // Marcar el estado como modificado

            try
            {
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationExists(id))
                {
                    return NotFound(); // Retornar 404 si la evaluación no existe
                }
                throw; // Propagar la excepción si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/evaluation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            // Eliminar una evaluación
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound(); // Retornar 404 si la evaluación no se encuentra
            }

            _context.Evaluations.Remove(evaluation); // Eliminar la evaluación
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool EvaluationExists(int id)
        {
            return _context.Evaluations.Any(e => e.Id == id); // Verificar si la evaluación existe
        }
    }
}
