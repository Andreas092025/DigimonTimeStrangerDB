using System.ComponentModel.DataAnnotations;

namespace DigimonDB.Models;

public class Move
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(50)]
    public string Type { get; set; } = string.Empty; // e.g., Fire, Water

    public int Power { get; set; }

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    // Many-to-many with Digimon
    public ICollection<DigimonMove> DigimonMoves { get; set; } = new List<DigimonMove>();
}