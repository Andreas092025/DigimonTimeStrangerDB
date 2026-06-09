using Spectre.Console;
using DigimonDB.Data;
using DigimonDB.Features.Digimon;
using DigimonDB.Features.Import;
using DigimonDB.Features.Item;
using DigimonDB.Features.Move;


namespace DigimonDB
{
    enum MainMenuChoice
    {
        ManageDigimon,
        ManageMoves,
        ManageItems,
        ImportData,
        Exit
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(
                new FigletText("Digimon Time Stranger DB")
                    .Centered()
                    .Color(Color.Blue));
            AnsiConsole.MarkupLine("[green]Welcome to the Digimon Story: Time Stranger Database![/]");
            AnsiConsole.MarkupLine("[green]Digimon from DLCs is included![/]");
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
                            MainMenuChoice.ManageItems => "Manage Items for Digivolutions",
                            MainMenuChoice.ImportData => "Import Data",
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