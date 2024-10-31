using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Participant
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
