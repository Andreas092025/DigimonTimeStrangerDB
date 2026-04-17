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
    enum MainMenuChoice
    {
        ManageDigimon,
        ManageMoves,
        ManageItems,
        ManageCharacters,
        ImportData,
        Exit
    }

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
                    new SelectionPrompt<MainMenuChoice>()
                        .Title("What would you like to do?")
                        .UseConverter(e => e switch
                        {
                            MainMenuChoice.ManageDigimon => "Manage Digimon",
                            MainMenuChoice.ManageMoves => "Manage Moves",
                            MainMenuChoice.ManageItems => "Manage Items",
                            MainMenuChoice.ManageCharacters => "Manage Characters",
                            MainMenuChoice.ImportData => "Import Sample Data",
                            MainMenuChoice.Exit => "Exit",
                            _ => e.ToString()
                        })
                        .AddChoices(Enum.GetValues<MainMenuChoice>().Cast<MainMenuChoice>()));

                switch (choice)
                {
                    case MainMenuChoice.ManageDigimon:
                        DigimonManager.ShowMenu(context);
                        break;
                    case MainMenuChoice.ManageMoves:
                        MoveManager.ShowMenu(context);
                        break;
                    case MainMenuChoice.ManageItems:
                        ItemManager.ShowMenu(context);
                        break;
                    case MainMenuChoice.ManageCharacters:
                        CharacterManager.ShowMenu(context);
                        break;
                    case MainMenuChoice.ImportData:
                        ImportManager.RunImport(context);
                        break;
                    case MainMenuChoice.Exit:
                        return;
                }
            }
        }

    }
}
