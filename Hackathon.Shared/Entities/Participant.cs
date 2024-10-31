using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hackathon.Shared.Entities
{
    public class Participant
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string FullName { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        [MaxLength(150, ErrorMessage = "El correo no puede tener más de 150 caracteres.")]
        public string Email { get; set; }

        [Display(Name = "ID del Equipo")]
        [Required(ErrorMessage = "El ID del equipo es obligatorio")]
        public int TeamId { get; set; }

        [JsonIgnore]
        public Team Team { get; set; }

        [Display(Name = "ID del Rol")]
        [Required(ErrorMessage = "El ID del rol es obligatorio")]
        public int RoleId { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }
    }
}
