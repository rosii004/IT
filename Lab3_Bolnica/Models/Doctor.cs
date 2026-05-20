using System.ComponentModel.DataAnnotations;

namespace Lab3_Bolnica.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }

        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}