using System;
using DigitalGameStore.Model;

namespace DigitalGameStore.Tools
{
	public class GameDisplay
	{
		public static void ShowGameDetails(GameObject game)
		{
			Console.Clear();
			Console.WriteLine(game.Name +
						  "\n\nPublisher:    " + game.Publisher +
						  "\nRelease date: " + game.ReleaseDate + " (YMD)" + 
						  "\nGenres/tags:  " + game.Genres[0] + ", " + game.Genres[1] + ", " + game.Genres[2] + ", " + game.Genres[3] + ", " + game.Genres[4]);

			Func.TextColor("blue");
			Console.WriteLine("\nPress ENTER to continue");
			Console.ResetColor();

			Console.ReadLine();
		}
	}
}

