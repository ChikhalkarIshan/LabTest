using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabTest.Models
{

    public class PatientViewModel
    {
        public int PatientId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

       
    }

    public class PatientLabTestViewModel
    {
        public int PatientId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<LabTestViewModel> PaitentlabTest { get; set; }
    }
}
