using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MedMinder_Api.Models
{
    public class Patient
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