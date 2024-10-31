using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Hackathon
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Hackathon")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Tema Principal")]
        [Required(ErrorMessage = "El tema principal es obligatorio")]
        [MaxLength(200, ErrorMessage = "El tema principal no puede tener más de 200 caracteres.")]
        public string MainTheme { get; set; }

        [JsonIgnore]
        public List<Team> Teams { get; set; } = new List<Team>();

        [JsonIgnore]
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        [JsonIgnore]
        public List<Award> Awards { get; set; } = new List<Award>();

        [JsonIgnore]
        public List<Sponsor> Sponsors { get; set; } = new List<Sponsor>();
    }
}
