using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Rol")]
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre del rol no puede tener más de 50 caracteres.")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Participant> Participants { get; set; } = new List<Participant>();
    }
}
