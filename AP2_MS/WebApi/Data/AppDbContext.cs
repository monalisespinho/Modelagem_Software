
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tutor> Tutores { get; set; }
    public DbSet<Pet> Pets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tutor>()
            .HasMany(t => t.Pets)
            .WithOne(p => p.Tutor)
            .HasForeignKey(p => p.TutorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}