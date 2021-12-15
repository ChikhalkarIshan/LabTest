using System.Text.Json.Serialization;

namespace LabTest.Models
{
    public class LabTestViewModel
    {
        public int LabTestId { get; set; }

        public int PatientId { get; set; }

        public int TypeOfTestId { get; set; }

        public string? Result { get; set; }

        public string? TimeOfTest { get; set; }

        public string? EnteredTime { get; set; }

        public DateTime DateTimeOfTest { get; set; }

    }
}
