using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Sponsor
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Contribution { get; set; }

        public int HackathonId { get; set; }
        public Hackathon Hackathon { get; set; }
    }
}
