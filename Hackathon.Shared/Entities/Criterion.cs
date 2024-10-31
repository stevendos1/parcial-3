using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Criterion
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
