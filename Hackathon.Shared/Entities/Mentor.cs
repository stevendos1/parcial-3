using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Mentor
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
