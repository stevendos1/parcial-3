using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Proyecto")]
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre del proyecto no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Estado del Proyecto")]
        [MaxLength(50, ErrorMessage = "El estado no puede tener más de 50 caracteres.")]
        public string Status { get; set; }

        [Display(Name = "ID del Equipo")]
        public int TeamId { get; set; }

        [JsonIgnore]
        public Team Team { get; set; }

        [JsonIgnore]
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
