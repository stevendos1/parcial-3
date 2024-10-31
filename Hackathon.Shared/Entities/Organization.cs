using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de la Organización")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Descripción de la Organización")]
        [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string Description { get; set; }
    }
}
