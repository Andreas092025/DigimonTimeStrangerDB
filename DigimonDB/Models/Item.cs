using System.ComponentModel.DataAnnotations;

namespace DigimonDB.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Type { get; set; } 

    public string Description { get; set; } 
}