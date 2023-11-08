using System;
using System.Collections.Generic;
using DigitalGameStore.DB;

namespace DigitalGameStore.MVC.Views
{
    public class BrowseView
    {
        public void DisplayGameList(IEnumerable<Game> games, int currentIndex, int totalCount)
        {
            Console.WriteLine("\nAvailable Games:\n");
            foreach (var game in games)
            {
                Console.WriteLine($"ID: {game.ID} - Name: {game.Name}");
            }

            Console.WriteLine("\nNavigation:");
            if (currentIndex > 1)
            {
                Console.WriteLine("Press 'P' for Previous Page");
            }

            if (currentIndex < totalCount)
            {
                Console.WriteLine("Press 'N' for Next Page");
            }

            Console.WriteLine("Enter the ID of the game to see details, or 'Q' to go back to the main menu.");
        }

        public void DisplayGameDetails(Game game)
        {
            Console.WriteLine($"\nGame Details for {game.Name}:");
            Console.WriteLine($"ID: {game.ID}");
            Console.WriteLine($"Name: {game.Name}");
            // Add more details as needed
            Console.WriteLine("Press 'Q' to go back to the list.");
        }

        public void DisplayError(string message)
        {
            Console.WriteLine($"\nError: {message}");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine($"\n{message}");
        }
    }
}
