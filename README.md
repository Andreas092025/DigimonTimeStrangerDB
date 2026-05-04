# Digimon Story Time Stranger Database

A database application for managing Digimon Story: Time Stranger data, built with C# .NET 10 and SQLite. Includes DLC Digimon.

## Features

- **Digimon**: Full CRUD — add, list, edit, delete with stats (HP, ATK, DEF, SPD, INT, Type, Level, Description).
- **Items**: Add, list, and delete items.
- **Moves**: Placeholder — not yet implemented.
- **Import**: Import sample data from `sample-data.json` (Digimon + Moves) via the in-app menu.
- **Python Bulk Import**: `PyImport/Import.py` bulk-imports 475+ Digimon from chunked JSON files (`digimon_1-50.json` … `digimon_451-475.json` + `items.json`) directly into the SQLite DB.
- Beautiful console UI with Spectre.Console.

## Setup

1. Ensure .NET 10 is installed.
2. Clone or download the project.
3. Run `dotnet build` in the `DigimonDB` directory.
4. Run `dotnet run` to start the application.

## Usage

- Use the menu to navigate.
- Run **Import Sample Data** to populate Digimon and Moves from `sample-data.json`.
- Or run `PyImport/Import.py` to bulk-import all Digimon from the chunked JSON files (Digimon and Items).
- Add, edit, or delete Digimon and Items via the menu.

## Future Plans

- Implement full CRUD for Moves.
- Add Item editing.
- Web scraping for real data from a Digimon wiki.
- Add a Digievoltion planner in some shape or form
- Implement Evolotions such as DNA evotion, Armor, Spirit etc.

## Dependencies

- `Microsoft.EntityFrameworkCore.Sqlite` 10.0.0
- `Microsoft.EntityFrameworkCore.Tools` 10.0.0
- `Spectre.Console` 0.49.1