using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Criterion
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Criterio")]
        [Required(ErrorMessage = "El nombre del criterio es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre del criterio no puede tener m�s de 100 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Puntuaci�n M�xima")]
        public int MaxScore { get; set; }

        [Display(Name = "ID de Evaluaci�n")]
        public int EvaluationId { get; set; }

        [JsonIgnore]
        public Evaluation Evaluation { get; set; }
    }
}
