using DigimonDB.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Core.Data;

public class DigimonContext : DbContext
{
    public DigimonContext(DbContextOptions<DigimonContext> options)
        : base(options)
    {
    }

    public DbSet<Digimon> Digimons { get; set; } = null!;
    public DbSet<Move> Moves { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Evolution> Evolutions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evolution>()
            .HasOne(e => e.FromDigimon)
            .WithMany(d => d.EvolutionsFrom)
            .HasForeignKey(e => e.FromDigimonId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Evolution>()
            .HasOne(e => e.ToDigimon)
            .WithMany(d => d.EvolutionsTo)
            .HasForeignKey(e => e.ToDigimonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
