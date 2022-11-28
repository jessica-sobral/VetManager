using Microsoft.EntityFrameworkCore;

namespace VetManager.Models;

public class VetManagerContext : DbContext
{
    public VetManagerContext(DbContextOptions<VetManagerContext> options) : base(options) { }
}