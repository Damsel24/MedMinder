using System.ComponentModel.DataAnnotations;

namespace MedMinder_Api.Dtos
{
    public class PatientCreateDto
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Active { get; set; }
    }
}
