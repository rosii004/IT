using System.ComponentModel.DataAnnotations;
namespace Lab3_Bolnica.Models;

public class Patient
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името е задолжително")]
    public string FullName { get; set; }

    [Range(10000, 99999, ErrorMessage = "Кодот мора да биде точно 5 цифри")]
    [Display(Name = "Код на пациентот")]
    public int PatientCode { get; set; }

    public string Gender { get; set; }

    // Пациентот може да оди кај МНОГУ доктори
    public ICollection<Doctor> Doctors { get; set; }
}