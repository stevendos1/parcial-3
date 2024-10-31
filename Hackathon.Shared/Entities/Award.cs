using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Award
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int HackathonId { get; set; }
        public Hackathon Hackathon { get; set; }
    }
}
