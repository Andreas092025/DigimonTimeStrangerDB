using System.ComponentModel.DataAnnotations;

namespace DigimonDB.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string EvolvesFrom { get; set; } = string.Empty; // Name of the Digimon this item evolves from
    public string EvolvesTo { get; set; } = string.Empty; // Name of the Digimon this item evolves to
}