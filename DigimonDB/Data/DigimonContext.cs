using Microsoft.EntityFrameworkCore;
using DigimonDB.Models;

namespace DigimonDB.Data;

public class DigimonContext : DbContext
{
    public DbSet<Digimon> Digimons { get; set; } = null!;
    public DbSet<Move> Moves { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Evolution> Evolutions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=digimon.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Evolution
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