// See https://aka.ms/new-console-template for more information
using Spectre.Console;
using DigimonDB.Data;
using DigimonDB.Models;
using DigimonDB.Features.Character;
using DigimonDB.Features.Digimon;
using DigimonDB.Features.Import;
using DigimonDB.Features.Item;
using DigimonDB.Features.Move;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DigimonDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(
                new FigletText("Digimon DB")
                    .Centered()
                    .Color(Color.Blue));

            AnsiConsole.MarkupLine("[green]Welcome to the Digimon Story: Time Stranger Database![/]");

            // Initialize DB
            using var context = new DigimonContext();
            context.Database.EnsureCreated();

            // Main menu
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices(new[] {
                            "Manage Digimon",
                            "Manage Moves",
                            "Manage Items",
                            "Manage Characters",
                            "Import Data",
                            "Exit"
                        }));

                switch (choice)
                {
                    case "Manage Digimon":
                        DigimonManager.ShowMenu(context);
                        break;
                    case "Manage Moves":
                        MoveManager.ShowMenu(context);
                        break;
                    case "Manage Items":
                        ItemManager.ShowMenu(context);
                        break;
                    case "Manage Characters":
                        CharacterManager.ShowMenu(context);
                        break;
                    case "Import Data":
                        ImportManager.RunImport(context);
                        break;
                    case "Exit":
                        return;
                }
            }
        }

    }
}
