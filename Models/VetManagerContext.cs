using Microsoft.EntityFrameworkCore;

namespace VetManager.Models;

public class VetManagerContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Tutor> Tutors{ get; set; }

    public VetManagerContext(DbContextOptions<VetManagerContext> options) : base(options) { }
}