using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de la Organizaci�n")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener m�s de 100 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Descripci�n de la Organizaci�n")]
        [MaxLength(500, ErrorMessage = "La descripci�n no puede tener m�s de 500 caracteres.")]
        public string Description { get; set; }
    }
}
