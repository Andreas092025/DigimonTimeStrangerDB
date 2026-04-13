using Spectre.Console;
using DigimonDB.Data;
using DigimonDB.Models;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Features.Digimon;

public static class DigimonManager
{
    public static void ShowMenu(DigimonContext context)
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Digimon Management")
                    .AddChoices(new[] {
                        "Add Digimon",
                        "List Digimon",
                        "Edit Digimon",
                        "Delete Digimon",
                        "Back to Main Menu"
                    }));

            switch (choice)
            {
                case "Add Digimon":
                    AddDigimon(context);
                    break;
                case "List Digimon":
                    ListDigimon(context);
                    break;
                case "Edit Digimon":
                    EditDigimon(context);
                    break;
                case "Delete Digimon":
                    DeleteDigimon(context);
                    break;
                case "Back to Main Menu":
                    return;
            }
        }
    }

    static void AddDigimon(DigimonContext context)
    {
        var name = AnsiConsole.Ask<string>("Enter Digimon name:");
        var type = AnsiConsole.Ask<string>("Enter type (e.g., Vaccine):");
        var level = AnsiConsole.Ask<int>("Enter level:");
        var hp = AnsiConsole.Ask<int>("Enter HP:");
        var atk = AnsiConsole.Ask<int>("Enter ATK:");
        var def = AnsiConsole.Ask<int>("Enter DEF:");
        var spd = AnsiConsole.Ask<int>("Enter SPD:");
        var intelligence = AnsiConsole.Ask<int>("Enter INT:");
        var description = AnsiConsole.Ask<string>("Enter description:");

        var digimon = new DigimonDB.Models.Digimon
        {
            Name = name,
            Type = type,
            Level = level,
            HP = hp,
            ATK = atk,
            DEF = def,
            SPD = spd,
            INT = intelligence,
            Description = description
        };

        context.Digimons.Add(digimon);
        context.SaveChanges();

        AnsiConsole.MarkupLine("[green]Digimon added successfully![/]");
    }

    static void ListDigimon(DigimonContext context)
    {
        var digimons = context.Digimons.ToList();
        if (!digimons.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No Digimon found.[/]");
            return;
        }

        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Type");
        table.AddColumn("Level");
        table.AddColumn("HP");
        table.AddColumn("ATK");
        table.AddColumn("DEF");
        table.AddColumn("SPD");
        table.AddColumn("INT");

        foreach (var d in digimons)
        {
            table.AddRow(d.Id.ToString(), d.Name, d.Type, d.Level.ToString(), d.HP.ToString(), d.ATK.ToString(), d.DEF.ToString(), d.SPD.ToString(), d.INT.ToString());
        }

        AnsiConsole.Write(table);
    }

    static void EditDigimon(DigimonContext context)
    {
        var id = AnsiConsole.Ask<int>("Enter Digimon ID to edit:");
        var digimon = context.Digimons.Find(id);
        if (digimon == null)
        {
            AnsiConsole.MarkupLine("[red]Digimon not found.[/]");
            return;
        }

        digimon.Name = AnsiConsole.Prompt(new TextPrompt<string>("Name:").DefaultValue(digimon.Name));
        digimon.Type = AnsiConsole.Prompt(new TextPrompt<string>("Type:").DefaultValue(digimon.Type));
        digimon.Level = AnsiConsole.Prompt(new TextPrompt<int>("Level:").DefaultValue(digimon.Level));
        digimon.HP = AnsiConsole.Prompt(new TextPrompt<int>("HP:").DefaultValue(digimon.HP));
        digimon.ATK = AnsiConsole.Prompt(new TextPrompt<int>("ATK:").DefaultValue(digimon.ATK));
        digimon.DEF = AnsiConsole.Prompt(new TextPrompt<int>("DEF:").DefaultValue(digimon.DEF));
        digimon.SPD = AnsiConsole.Prompt(new TextPrompt<int>("SPD:").DefaultValue(digimon.SPD));
        digimon.INT = AnsiConsole.Prompt(new TextPrompt<int>("INT:").DefaultValue(digimon.INT));
        digimon.Description = AnsiConsole.Prompt(new TextPrompt<string>("Description:").DefaultValue(digimon.Description));

        context.SaveChanges();
        AnsiConsole.MarkupLine("[green]Digimon updated successfully![/]");
    }

    static void DeleteDigimon(DigimonContext context)
    {
        var id = AnsiConsole.Ask<int>("Enter Digimon ID to delete:");
        var digimon = context.Digimons.Find(id);
        if (digimon == null)
        {
            AnsiConsole.MarkupLine("[red]Digimon not found.[/]");
            return;
        }

        if (AnsiConsole.Confirm($"Are you sure you want to delete {digimon.Name}?"))
        {
            context.Digimons.Remove(digimon);
            context.SaveChanges();
            AnsiConsole.MarkupLine("[green]Digimon deleted successfully![/]");
        }
    }
}
