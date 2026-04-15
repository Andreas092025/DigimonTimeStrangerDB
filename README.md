# Digimon Story Time Stranger Database

A simple database application for managing Digimon Story: Time Stranger data, built with C# .NET 10 and SQLite.

## Features

- Manage Digimon: Add, list, edit, delete Digimon with stats.
- Manage Moves, Items, Characters (placeholders for now).
- Import sample data from JSON.
- Beautiful console UI with Spectre.Console.

## Setup

1. Ensure .NET 10 is installed.
2. Clone or download the project.
3. Run `dotnet build` in the DigimonDB directory.
4. Run `dotnet run` to start the application.

## Usage

- Use the menu to navigate.
- Import data first to populate sample Digimon.
- Add your own Digimon via the menu.
- Has a SQlite extention for VScode.

## Future Plans

- Implement full CRUD for Moves, Items, Characters.
- Add Python version using the same SQLite DB.
- Web scraping for more data.
- Fix so it's possible to edit DB if something unexpeted happened. 

## Dependencies

- Microsoft.EntityFrameworkCore.Sqlite
- Spectre.Console
