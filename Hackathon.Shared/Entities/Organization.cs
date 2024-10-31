using System.ComponentModel.DataAnnotations;

namespace Hackathon.Shared.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}
