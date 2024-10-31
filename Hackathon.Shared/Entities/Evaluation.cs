using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Evaluation
    {
        public int Id { get; set; }

        [Range(0, 100)]
        public int Score { get; set; }

        [MaxLength(500)]
        public string Feedback { get; set; }

        public DateTime EvaluationDate { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
