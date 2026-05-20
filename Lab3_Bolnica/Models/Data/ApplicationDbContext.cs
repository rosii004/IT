using Microsoft.EntityFrameworkCore;
using Lab3_Bolnica.Models;

namespace Lab3_Bolnica.Models.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }
    
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

}