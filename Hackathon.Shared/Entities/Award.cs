using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Award
    {
        public int Id { get; set; }

        [Display(Name = "Título del Premio")]
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Title { get; set; }

        [Display(Name = "Descripción del Premio")]
        [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string Description { get; set; }

        [Display(Name = "ID del Hackathon")]
        [Required(ErrorMessage = "El ID de Hackathon es obligatorio")]
        public int HackathonId { get; set; }

        [JsonIgnore]
        public Hackathon Hackathon { get; set; }
    }
}
