# Star Wars Ships

## Objective

Star Wars Ships is a code challenge with the objective of measuring, given a distance, how many stops for resupply are required for each Star Wars Ship.
This project is built in .NET Core 2.0 and it is based on a Star Wars API (available in https://swapi.co/) that is responsible for providing a list of Star Wars Ships with information like ship name, the maximum length of time that this starship can provide consumables for its entire crew without having to resupply and the maximum number of Megalights this starship can travel in a standard hour. A "Megalight" is a standard unit of distance and has never been defined before within the Star Wars universe.
The project is responsible for accept the distance user input in mega lights (MGLT) and calculate the how many stops for resupply are required over all star ship listed on API.

## Usage
For use this source code, you must have installed Visual Studio 2017 with .NET Core 2.0 configured and download or clone the source code to your PC and open it on Visual Studio 2017.
- Link for download: https://visualstudio.microsoft.com/vs/

## Host
The project is hosted on Azure App Services and it can be accessed by the following link:
- https://swsc.azurewebsites.net/Starship/GetDistance

## Technologies
For this project, the following technologies were chosen:
- Visual Studio 2017
- C#
- ASP.NET Core 2.0
- ASP.NET MVC Core
- HTML5
- CSS
- JS/jQuery
- Bootstrap

## Architecture
For a better maintainability, was chosen Multi-tier Architecture with segregation of responsibilities so the project is divided in Presentation, Business, Data Access (persistence), Entities and Tests Tier. The choice was made to help with Clean Code techniques and SOLID Principles. Additionally, the project is covered by unit tests to improve the reliable of the project.
