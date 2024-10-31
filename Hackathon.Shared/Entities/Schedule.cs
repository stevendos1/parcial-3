using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [MaxLength(200)]
        public string Activity { get; set; }

        public int HackathonId { get; set; }
        public Hackathon Hackathon { get; set; }
    }
}
