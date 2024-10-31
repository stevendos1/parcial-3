using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Hackathon
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, MaxLength(200)]
        public string MainTheme { get; set; }

        public ICollection<Team> Teams { get; set; }
        public ICollection<Mentor> Mentors { get; set; }
        public ICollection<Award> Awards { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<Sponsor> Sponsors { get; set; }
    }
}
