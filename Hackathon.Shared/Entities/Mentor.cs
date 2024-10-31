using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Mentor
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Mentor")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
