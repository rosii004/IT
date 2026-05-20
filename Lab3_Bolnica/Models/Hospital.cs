namespace Lab3_Bolnica.Models;

public class Hospital
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string ImageUrl { get; set; }

    // Една болница има многу доктори
    public ICollection<Doctor> Doctors { get; set; }
}