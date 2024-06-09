// using System.ComponentModel.DataAnnotations;

// namespace MedMinder_Api.Dtos
// {
//     public class PatientReadDto
//     {
        
//         public int PatientId { get; set; }

//         public string? FirstName { get; set; }

//         public string? LastName { get; set; }

//         public string? City { get; set; }

//         public bool? Active { get; set; }
//     }
    
// }

using System.ComponentModel.DataAnnotations;

namespace MedMinder_Api.Dtos
{
    public class PatientReadDto
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public bool? Active { get; set; }
    }
}
