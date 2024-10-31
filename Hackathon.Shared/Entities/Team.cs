using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public int HackathonId { get; set; }
        public Hackathon Hackathon { get; set; }

        public ICollection<Participant> Participants { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
