using System.ComponentModel.DataAnnotations;

namespace DigimonDB.Models;

public class Character
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(50)]
    public string Role { get; set; } = string.Empty; // e.g., Protagonist, Ally

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}