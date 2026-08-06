using System.ComponentModel.DataAnnotations;

namespace DigimonDB.Core.Models;

public class Digimon
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(50)]
    public string Type { get; set; } = string.Empty;

    public int Level { get; set; }

    public int HP { get; set; }
    public int ATK { get; set; }
    public int DEF { get; set; }
    public int SPD { get; set; }
    public int INT { get; set; }

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public ICollection<Evolution> EvolutionsFrom { get; set; } = [];
    public ICollection<Evolution> EvolutionsTo { get; set; } = [];
}
