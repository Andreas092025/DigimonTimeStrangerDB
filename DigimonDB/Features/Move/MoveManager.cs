using Spectre.Console;
using DigimonDB.Data;
using DigimonDB.Models;

namespace DigimonDB.Features.Move;

public static class MoveManager
{
    public static void ShowMenu(DigimonContext context)
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Move Management")
                    .AddChoices(new[] {
                        "List Moves",
                        "Back to Main Menu"
                    }));

            switch (choice)
            {
                case "List Moves":
                    ListMoves(context);
                    break;
                case "Back to Main Menu":
                    return;
            }
        }
    }

    static void AddMove(DigimonContext context)
    {
        var name = AnsiConsole.Ask<string>("Enter move name:");
        var type = AnsiConsole.Ask<string>("Enter move type (e.g., Fire, Water, Physical):");
        var description = AnsiConsole.Ask<string>("Enter description:");

        var move = new Models.Move
        {
            Name = name,
            Type = type,
            Description = description
        };

        context.Moves.Add(move);
        context.SaveChanges();

        AnsiConsole.MarkupLine("[green]Move added successfully![/]");
    }

    static void ListMoves(DigimonContext context)
    {
        var moves = context.Moves.ToList();
        if (!moves.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No moves found.[/]");
            return;
        }

        var table = new Table();
        table.Expand();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Type");
        table.AddColumn("Description");

        foreach (var m in moves)
        {
            // Escape square brackets in description to prevent Spectre markup parsing
            var escapedDescription = m.Description.Replace("[", "[[").Replace("]", "]]");
            table.AddRow(m.Id.ToString(), m.Name, m.Type, escapedDescription);
        }

        AnsiConsole.Write(table);
    }

    static void EditMove(DigimonContext context)
    {
        var id = AnsiConsole.Ask<int>("Enter Move ID to edit:");
        var move = context.Moves.Find(id);
        if (move == null)
        {
            AnsiConsole.MarkupLine("[red]Move not found.[/]");
            return;
        }

        move.Name = AnsiConsole.Prompt(new TextPrompt<string>("Name:").DefaultValue(move.Name));
        move.Type = AnsiConsole.Prompt(new TextPrompt<string>("Type:").DefaultValue(move.Type));
        move.Description = AnsiConsole.Prompt(new TextPrompt<string>("Description:").DefaultValue(move.Description));

        context.SaveChanges();
        AnsiConsole.MarkupLine("[green]Move updated successfully![/]");
    }

    static void DeleteMove(DigimonContext context)
    {
        var id = AnsiConsole.Ask<int>("Enter Move ID to delete:");
        var move = context.Moves.Find(id);
        if (move == null)
        {
            AnsiConsole.MarkupLine("[red]Move not found.[/]");
            return;
        }

        if (AnsiConsole.Confirm($"Are you sure you want to delete {move.Name}?"))
        {
            context.Moves.Remove(move);
            context.SaveChanges();
            AnsiConsole.MarkupLine("[green]Move deleted successfully![/]");
        }
    }
}
