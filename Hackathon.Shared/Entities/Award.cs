using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Award
    {
        public int Id { get; set; }

        [Display(Name = "T�tulo del Premio")]
        [Required(ErrorMessage = "El t�tulo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El t�tulo no puede tener m�s de 100 caracteres.")]
        public string Title { get; set; }

        [Display(Name = "Descripci�n del Premio")]
        [MaxLength(500, ErrorMessage = "La descripci�n no puede tener m�s de 500 caracteres.")]
        public string Description { get; set; }

        [Display(Name = "ID del Hackathon")]
        [Required(ErrorMessage = "El ID de Hackathon es obligatorio")]
        public int HackathonId { get; set; }

        [JsonIgnore]
        public Hackathon Hackathon { get; set; }
    }
}
