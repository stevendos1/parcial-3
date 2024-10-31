using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
