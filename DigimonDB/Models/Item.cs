using System.ComponentModel.DataAnnotations;

namespace DigimonDB.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(50)]
    public string Type { get; set; } = string.Empty; // e.g., Consumable, Equipment

    [StringLength(500)]
    public string Effect { get; set; } = string.Empty;
}