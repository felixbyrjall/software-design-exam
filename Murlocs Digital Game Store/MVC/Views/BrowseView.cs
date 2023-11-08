using System;
using System.Collections.Generic;
using DigitalGameStore.DB;

namespace DigitalGameStore.Views
{
    public class BrowseView
    {
        public void DisplayGameList(IEnumerable<Game> games)
        {
            Console.WriteLine("\nHere's all games:\n");
            foreach (var game in games)
            {
                Console.WriteLine($"ID: {game.ID} - Name: {game.Name}");
            }
        }
    }
}
