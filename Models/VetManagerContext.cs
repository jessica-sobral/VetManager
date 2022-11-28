using Microsoft.EntityFrameworkCore;

namespace VetManager.Models;

public class VetManagerContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }

    public VetManagerContext(DbContextOptions<VetManagerContext> options) : base(options) { }
}