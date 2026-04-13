namespace DigimonDB.Models;

public class DigimonMove
{
    public int DigimonId { get; set; }
    public Digimon Digimon { get; set; } = null!;

    public int MoveId { get; set; }
    public Move Move { get; set; } = null!;
}