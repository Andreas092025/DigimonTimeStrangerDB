# Digimon Time Stranger Database

A .NET-based database and UI project for managing Digimon Story: Time Stranger data, built around SQLite and a .NET MAUI front end.

## Overview

This repository contains two related pieces:

- A data layer and import pipeline in the console project under [DigimonDB](DigimonDB)
- A MAUI application in [DigimonDB.App](DigimonDB.App) for browsing and exploring data from the database
- Shared domain and service logic in [DigimonDB.Core](DigimonDB.Core)

The app is currently focused on cataloging Digimon, moves, items, and evolution relationships using local JSON seed data and SQLite.

## Current Feature Set

- SQLite-backed database with tables for Digimon, Moves, Items, and Evolutions
- MAUI navigation with dedicated pages for:
  - Database dashboard
  - Digimon catalog
  - Items
  - Digivolution planner
  - About information
- Dashboard counters for total Digimon, moves, items, and evolutions
- Seed data import from bundled JSON files into the SQLite database
- Evolution lookup for direct evolutions from a chosen Digimon
- Pathfinding between start and target Digimon through stored evolution edges
- Local file logging and app data persistence for desktop runtime use

## Project Structure

- [DigimonDB](DigimonDB)
  - Console/data project
  - EF Core SQLite context and model definitions
  - JSON seed data and import tooling
- [DigimonDB.Core](DigimonDB.Core)
  - Shared services and DTO/model layer used by the app
  - Contains `DigimonService`, `ItemService`, `ImportService`, `DashboardService## Flowchart
[![](https://mermaid.ink/img/pako:eNp1kkGPmzAQhf8K8mkrJRE2gRAOlTaAIqrm0t1TTQ7WMiFuwY6MabOL8t_r4DibtF1ODN-8mfdGDOhFVoAStGvk75c9U9p7zkrhmefxgT5p82H7yZtOP3srmvHu0LBX75nrBspSZLzmrRSmbsF70oqJGpSXrbZWvxplKS0E15w1_A28iyKVQsNRmxG56HoFqQKmobro0lGXDRvGhbcB0Z9KYUk2kpxumGD1ddr2Fq4dLDS03R0qaNEepAmYMc3uyJcHmh-5CeoW5XYRHlzEi42RYQsJfayqvzw4FtCvvNMfwDnNK_4RDGkGDWj4P86cwbUNi4cx5q29te1cW3tnvL0HF2-393Fo7pb_K7tuLuwtMf0GrPJmPzpznR1vwM0qbH9Brte2Qa6cWB44vpG_3sWBhXMHb10Wc-sETVCteIUSrXqYoBZUy84lGs59JdJ7aKFEiXmtmPpZolKcjObAxHcpWydTsq_3KNmxpjNVf6jMH5hxViv23gKiApXKXmiUYEzIOAQlAzqiJCYzPyKRH0ckjudhiCfoFSXT2J9hn0QLfxmSaBkRcpqgt3EtnuEwCsKALOPFAhMz7vQHlGkFvQ?type=png)](https://mermaid.live/edit#pako:eNp1kkGPmzAQhf8K8mkrJRE2gRAOlTaAIqrm0t1TTQ7WMiFuwY6MabOL8t_r4DibtF1ODN-8mfdGDOhFVoAStGvk75c9U9p7zkrhmefxgT5p82H7yZtOP3srmvHu0LBX75nrBspSZLzmrRSmbsF70oqJGpSXrbZWvxplKS0E15w1_A28iyKVQsNRmxG56HoFqQKmobro0lGXDRvGhbcB0Z9KYUk2kpxumGD1ddr2Fq4dLDS03R0qaNEepAmYMc3uyJcHmh-5CeoW5XYRHlzEi42RYQsJfayqvzw4FtCvvNMfwDnNK_4RDGkGDWj4P86cwbUNi4cx5q29te1cW3tnvL0HF2-393Fo7pb_K7tuLuwtMf0GrPJmPzpznR1vwM0qbH9Brte2Qa6cWB44vpG_3sWBhXMHb10Wc-sETVCteIUSrXqYoBZUy84lGs59JdJ7aKFEiXmtmPpZolKcjObAxHcpWydTsq_3KNmxpjNVf6jMH5hxViv23gKiApXKXmiUYEzIOAQlAzqiJCYzPyKRH0ckjudhiCfoFSXT2J9hn0QLfxmSaBkRcpqgt3EtnuEwCsKALOPFAhMz7vQHlGkFvQ)


## ClassDiagram

[![](https://mermaid.ink/img/pako:eNqtVN9vmzAQ_lese9wogoRA8MOkKbRqtC2qVJ4qXqxwJVbBjoxJ10XZ3z7jEJKStGql-YW77_zdj--ALSxljkBhWbK6TjgrFKsyQcyxCEl4wSspyHYPtucrF5rM8xOg1oqLgixYhedo-rLGAfknbrAcYLd3A-B7-mOAJNc3A-T-Lhn2tUjPW0iwXiq-1lyKk-B8JssSly3693ojy8ZapDfrGyWrz9xP5f72LhOnEvYX3hexBdqKneJnsVReinQTzqTI-WC-w-pOkl6I9mkv9j7XWP2_3b-1Dlt0bx7aysDPgFxdfTOW57pfjHPUkVp7gzV57Ff0SaLudnWMXSDa6Y-cZ65XH2AdOqFkgc_klbo24Ucp4ECheA5UqwYdqFBVrHXB7iMDvUKjOVBj5kw9ZZCJneGsmXiQsjrQlGyKFdBHVtbGa9Y509h95z2qUOSoZrIRGqjvBSObBegWfht_4rlh4EW-F08m0yAMQwdegE4jNx57URQE4ziOo2i0c-CPreu5UTwJR1PfsEZ-OPamDqB5O6X61f1r2sfuH6ZiUvw?type=png)](https://mermaid.live/edit#pako:eNqtVN9vmzAQ_lese9wogoRA8MOkKbRqtC2qVJ4qXqxwJVbBjoxJ10XZ3z7jEJKStGql-YW77_zdj--ALSxljkBhWbK6TjgrFKsyQcyxCEl4wSspyHYPtucrF5rM8xOg1oqLgixYhedo-rLGAfknbrAcYLd3A-B7-mOAJNc3A-T-Lhn2tUjPW0iwXiq-1lyKk-B8JssSly3693ojy8ZapDfrGyWrz9xP5f72LhOnEvYX3hexBdqKneJnsVReinQTzqTI-WC-w-pOkl6I9mkv9j7XWP2_3b-1Dlt0bx7aysDPgFxdfTOW57pfjHPUkVp7gzV57Ff0SaLudnWMXSDa6Y-cZ65XH2AdOqFkgc_klbo24Ucp4ECheA5UqwYdqFBVrHXB7iMDvUKjOVBj5kw9ZZCJneGsmXiQsjrQlGyKFdBHVtbGa9Y509h95z2qUOSoZrIRGqjvBSObBegWfht_4rlh4EW-F08m0yAMQwdegE4jNx57URQE4ziOo2i0c-CPreu5UTwJR1PfsEZ-OPamDqB5O6X61f1r2sfuH6ZiUvw)
