using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Sponsor
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Patrocinador")]
        [Required(ErrorMessage = "El nombre del patrocinador es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Contribución del Patrocinador")]
        [MaxLength(200, ErrorMessage = "La contribución no puede tener más de 200 caracteres.")]
        public string Contribution { get; set; }

        [Display(Name = "ID del Hackathon")]
        public int HackathonId { get; set; }

        [JsonIgnore]
        public Hackathon Hackathon { get; set; }
    }
}
