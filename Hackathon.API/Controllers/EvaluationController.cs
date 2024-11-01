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
            // Buscar una evaluaci�n por ID
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra la evaluaci�n
            }
            return Ok(evaluation); // Retornar la evaluaci�n encontrada
        }

        // POST: api/evaluation
        [HttpPost]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        {
            // Crear una nueva evaluaci�n
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return CreatedAtAction(nameof(GetEvaluation), new { id = evaluation.Id }, evaluation); // Retornar 201 Created
        }

        // PUT: api/evaluation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluation(int id, Evaluation evaluation)
        {
            // Actualizar una evaluaci�n existente
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
                    return NotFound(); // Retornar 404 si la evaluaci�n no existe
                }
                throw; // Propagar la excepci�n si ocurre otro error
            }

            return NoContent(); // Retornar 204 No Content
        }

        // DELETE: api/evaluation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            // Eliminar una evaluaci�n
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound(); // Retornar 404 si la evaluaci�n no se encuentra
            }

            _context.Evaluations.Remove(evaluation); // Eliminar la evaluaci�n
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return NoContent(); // Retornar 204 No Content
        }

        private bool EvaluationExists(int id)
        {
            return _context.Evaluations.Any(e => e.Id == id); // Verificar si la evaluaci�n existe
        }
    }
}
