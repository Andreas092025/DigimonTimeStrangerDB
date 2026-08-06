using System.ComponentModel.DataAnnotations;

namespace DigimonDB.Core.Models;

public class Evolution
{
    public int Id { get; set; }

    public int FromDigimonId { get; set; }
    public Digimon FromDigimon { get; set; } = null!;

    public int ToDigimonId { get; set; }
    public Digimon ToDigimon { get; set; } = null!;

    [StringLength(200)]
    public string Condition { get; set; } = string.Empty;
}
