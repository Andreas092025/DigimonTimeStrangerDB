# Digimon Story Time Stranger Database

A database application for managing Digimon Story: Time Stranger data, built with C# .NET 10 and SQLite. Includes DLC Digimon.

## Features

- **Digimon**: Full CRUD — add, list, edit, delete with stats (HP, ATK, DEF, SPD, INT, Type, Level, Description).
- **Items**: Add, list, and delete items.
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
- Run **Import Sample Data** to populate Digimon and Items from `sample-data.json`.
- Or run `PyImport/Import.py` to bulk-import all Digimon from the chunked JSON files (Digimon and Items).
- Add, edit, or delete Digimon and Items via the menu.

## Future Plans

- Add Item editing (Adding/Editing/Deleting).
- Web scraping for real data from a Digimon wiki.
- Add a Digievoltion planner in some shape or form
- Implement Evolotions such as DNA evolotion, Armor, Spirit etc.

## Dependencies

- `Microsoft.EntityFrameworkCore.Sqlite` 
- `Microsoft.EntityFrameworkCore.Tools` 
- `Spectre.Console`

## Flowchart 

[![](https://mermaid.ink/img/pako:eNp1kkGPmzAQhf8K8mkrJRE2gRAOlTaAIqrm0t1TTQ7WMiFuwY6MabOL8t_r4DibtF1ODN-8mfdGDOhFVoAStGvk75c9U9p7zkrhmefxgT5p82H7yZtOP3srmvHu0LBX75nrBspSZLzmrRSmbsF70oqJGpSXrbZWvxplKS0E15w1_A28iyKVQsNRmxG56HoFqQKmobro0lGXDRvGhbcB0Z9KYUk2kpxumGD1ddr2Fq4dLDS03R0qaNEepAmYMc3uyJcHmh-5CeoW5XYRHlzEi42RYQsJfayqvzw4FtCvvNMfwDnNK_4RDGkGDWj4P86cwbUNi4cx5q29te1cW3tnvL0HF2-393Fo7pb_K7tuLuwtMf0GrPJmPzpznR1vwM0qbH9Brte2Qa6cWB44vpG_3sWBhXMHb10Wc-sETVCteIUSrXqYoBZUy84lGs59JdJ7aKFEiXmtmPpZolKcjObAxHcpWydTsq_3KNmxpjNVf6jMH5hxViv23gKiApXKXmiUYEzIOAQlAzqiJCYzPyKRH0ckjudhiCfoFSXT2J9hn0QLfxmSaBkRcpqgt3EtnuEwCsKALOPFAhMz7vQHlGkFvQ?type=png)](https://mermaid.live/edit#pako:eNp1kkGPmzAQhf8K8mkrJRE2gRAOlTaAIqrm0t1TTQ7WMiFuwY6MabOL8t_r4DibtF1ODN-8mfdGDOhFVoAStGvk75c9U9p7zkrhmefxgT5p82H7yZtOP3srmvHu0LBX75nrBspSZLzmrRSmbsF70oqJGpSXrbZWvxplKS0E15w1_A28iyKVQsNRmxG56HoFqQKmobro0lGXDRvGhbcB0Z9KYUk2kpxumGD1ddr2Fq4dLDS03R0qaNEepAmYMc3uyJcHmh-5CeoW5XYRHlzEi42RYQsJfayqvzw4FtCvvNMfwDnNK_4RDGkGDWj4P86cwbUNi4cx5q29te1cW3tnvL0HF2-393Fo7pb_K7tuLuwtMf0GrPJmPzpznR1vwM0qbH9Brte2Qa6cWB44vpG_3sWBhXMHb10Wc-sETVCteIUSrXqYoBZUy84lGs59JdJ7aKFEiXmtmPpZolKcjObAxHcpWydTsq_3KNmxpjNVf6jMH5hxViv23gKiApXKXmiUYEzIOAQlAzqiJCYzPyKRH0ckjudhiCfoFSXT2J9hn0QLfxmSaBkRcpqgt3EtnuEwCsKALOPFAhMz7vQHlGkFvQ)

## ClassDiagram
[![](https://mermaid.ink/img/pako:eNqtVN9vmzAQ_lese9wogoRA8MOkKbRqtC2qVJ4qXqxwJVbBjoxJ10XZ3z7jEJKStGql-YW77_zdj--ALSxljkBhWbK6TjgrFKsyQcyxCEl4wSspyHYPtucrF5rM8xOg1oqLgixYhedo-rLGAfknbrAcYLd3A-B7-mOAJNc3A-T-Lhn2tUjPW0iwXiq-1lyKk-B8JssSly3693ojy8ZapDfrGyWrz9xP5f72LhOnEvYX3hexBdqKneJnsVReinQTzqTI-WC-w-pOkl6I9mkv9j7XWP2_3b-1Dlt0bx7aysDPgFxdfTOW57pfjHPUkVp7gzV57Ff0SaLudnWMXSDa6Y-cZ65XH2AdOqFkgc_klbo24Ucp4ECheA5UqwYdqFBVrHXB7iMDvUKjOVBj5kw9ZZCJneGsmXiQsjrQlGyKFdBHVtbGa9Y509h95z2qUOSoZrIRGqjvBSObBegWfht_4rlh4EW-F08m0yAMQwdegE4jNx57URQE4ziOo2i0c-CPreu5UTwJR1PfsEZ-OPamDqB5O6X61f1r2sfuH6ZiUvw?type=png)](https://mermaid.live/edit#pako:eNqtVN9vmzAQ_lese9wogoRA8MOkKbRqtC2qVJ4qXqxwJVbBjoxJ10XZ3z7jEJKStGql-YW77_zdj--ALSxljkBhWbK6TjgrFKsyQcyxCEl4wSspyHYPtucrF5rM8xOg1oqLgixYhedo-rLGAfknbrAcYLd3A-B7-mOAJNc3A-T-Lhn2tUjPW0iwXiq-1lyKk-B8JssSly3693ojy8ZapDfrGyWrz9xP5f72LhOnEvYX3hexBdqKneJnsVReinQTzqTI-WC-w-pOkl6I9mkv9j7XWP2_3b-1Dlt0bx7aysDPgFxdfTOW57pfjHPUkVp7gzV57Ff0SaLudnWMXSDa6Y-cZ65XH2AdOqFkgc_klbo24Ucp4ECheA5UqwYdqFBVrHXB7iMDvUKjOVBj5kw9ZZCJneGsmXiQsjrQlGyKFdBHVtbGa9Y509h95z2qUOSoZrIRGqjvBSObBegWfht_4rlh4EW-F08m0yAMQwdegE4jNx57URQE4ziOo2i0c-CPreu5UTwJR1PfsEZ-OPamDqB5O6X61f1r2sfuH6ZiUvw)
