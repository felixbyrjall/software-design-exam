using DigitalGameStore.DB;
using DigitalGameStore.Models;
using System;

namespace DigitalGameStore.MVC.Views
{
    public class GameInfoView
    {
        public void DisplayGameDetails(Game game)
        {
            if (game == null)
            {
                Console.WriteLine("Game not found.");
                return;
            }

            Console.WriteLine("Game Details:");
            Console.WriteLine($"Name: {game.Name}");
            Console.WriteLine($"ID: {game.ID}");
            Console.WriteLine($"Publisher: {game.Publisher?.Name ?? "Unknown Publisher"}");
            Console.WriteLine($"Release Date: {game.ReleaseDate}"); // Assuming ReleaseDate is a string in the format of a date

            Console.WriteLine("Genres:");
            if (game.GameGenres != null)
            {
                foreach (var gameGenre in game.GameGenres)
                {
                    Console.WriteLine($"\t{gameGenre.Genres?.Name ?? "Unknown Genre"}");
                }
            }
            else
            {
                Console.WriteLine("\tNo genres listed.");
            }
            // ... Include more details as needed
        }

        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }
    }
}
