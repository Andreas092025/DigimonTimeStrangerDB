using Spectre.Console;
using DigimonDB.Data;
using DigimonDB.Models;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Features.Item;

public static class ItemManager
{
    public static void ShowMenu(DigimonContext context)
    {
        while (true) 
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Digivolution Items Management")
                    .AddChoices(new[] {
                        "Add Digivolution Item",
                        "List Digivolution Items",
                        "Delete Digivolution Item (in case of mistakes)",
                        "Back to Main Menu"
                    }));
            switch (choice)
            {
                case "Add Digivolution Item":
                    AddItem(context);
                    break;
                case "List Digivolution Items":
                    ListItems(context);
                    break;
                case "Delete Digivolution Item (in case of mistakes)":
                    DeleteItem(context);
                    break;
                case "Back to Main Menu":
                    return;
            }
        }
    }



static void AddItem(DigimonContext context)
    {
        var name = AnsiConsole.Ask<string>("Enter item name:");
        var type = AnsiConsole.Ask<string>("Enter item type (e.g., Evolution, Support):");
        var description = AnsiConsole.Ask<string>("Enter item description:");
        var evolvesFrom = AnsiConsole.Ask<string>("Enter the name of the Digimon this item evolves from (or leave blank):");
        var evolvesTo = AnsiConsole.Ask<string>("Enter the name of the Digimon this item evolves to (or leave blank):");

        var item = new DigimonDB.Models.Item
        {
            Name = name,
            Type = type,
            Description = description,
            EvolvesFrom = evolvesFrom,
            EvolvesTo = evolvesTo
        };
        context.Items.Add(item);
        context.SaveChanges();

        AnsiConsole.MarkupLine("[green]Item added successfully![/]");
    }
    static void ListItems(DigimonContext context)
    {
        var items = context.Items.ToList();
        if (items.Any() == false)
        
        {
            AnsiConsole.MarkupLine("[yellow]No items found.[/]");
            return;
        }

        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Type");
        table.AddColumn("Description");
        table.AddColumn("Evolves From");
        table.AddColumn("Evolves To");

        foreach (var item in items)
        {
            table.AddRow(item.Id.ToString(), item.Name, item.Type, item.Description, item.EvolvesFrom, item.EvolvesTo);
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[green] Items can be bought from both Volcanusmon or Gold Guardromon later in the game.[/]");
    }
    static void DeleteItem(DigimonContext context)
    {
        var items = context.Items.ToList();
        if (items.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No items to delete.[/]");
            return;
        }

        var itemNames = items.Select(i => i.Name).ToList();
        itemNames.Add("Back to Items Menu");

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an item to delete")
                .AddChoices(itemNames));

        if (choice == "Back to Items Menu")
            return;

        var itemToDelete = context.Items.FirstOrDefault(i => i.Name == choice);
        if (itemToDelete != null)
        {
            context.Items.Remove(itemToDelete);
            context.SaveChanges();
            AnsiConsole.MarkupLine("[green]Item deleted successfully![/]"); 
        }
    }
}
