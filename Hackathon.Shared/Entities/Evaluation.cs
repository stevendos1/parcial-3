using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Evaluation
    {
        public int Id { get; set; }

        [Display(Name = "Puntuación")]
        [Range(0, 100, ErrorMessage = "La puntuación debe estar entre 0 y 100.")]
        public int Score { get; set; }

        [Display(Name = "Comentarios")]
        [MaxLength(500, ErrorMessage = "Los comentarios no pueden tener más de 500 caracteres.")]
        public string Feedback { get; set; }

        [Display(Name = "Fecha de Evaluación")]
        [Required(ErrorMessage = "La fecha de evaluación es obligatoria")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EvaluationDate { get; set; }

        [Display(Name = "ID del Proyecto")]
        [Required(ErrorMessage = "El ID del proyecto es obligatorio")]
        public int ProjectId { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }

        [Display(Name = "ID del Mentor")]
        [Required(ErrorMessage = "El ID del mentor es obligatorio")]
        public int MentorId { get; set; }

        [JsonIgnore]
        public Mentor Mentor { get; set; }
        
        [JsonIgnore]
        public List<Criterion> Criteria { get; set; } = new List<Criterion>();
    }
}
