using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [Display(Name = "Fecha del Evento")]
        [Required(ErrorMessage = "La fecha del evento es obligatoria")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Actividad")]
        [MaxLength(200, ErrorMessage = "La actividad no puede tener más de 200 caracteres.")]
        public string Activity { get; set; }

        [Display(Name = "ID del Hackathon")]
        [Required(ErrorMessage = "El ID de Hackathon es obligatorio")]
        public int HackathonId { get; set; }

        [JsonIgnore]
        public Hackathon Hackathon { get; set; }
    }
}
