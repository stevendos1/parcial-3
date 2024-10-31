using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Equipo")]
        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre del equipo no puede tener más de 50 caracteres.")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Project> Projects { get; set; } = new List<Project>();

        [JsonIgnore]
        public List<Participant> Participants { get; set; } = new List<Participant>();

        [Display(Name = "ID del Hackathon")]
        public int HackathonId { get; set; } 

        [JsonIgnore]
        public HackathonEntity HackathonEntity { get; set; }
    }
}
