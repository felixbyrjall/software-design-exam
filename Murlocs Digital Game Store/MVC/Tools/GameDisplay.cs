using System;
using DigitalGameStore.Model;

namespace DigitalGameStore.Tools
{
	public class GameDisplay
	{
		public static void ShowGameDetails(GameObject game)
		{
			Console.Clear();
			Console.WriteLine("Name: " + game.Name +
						  "\n Publisher: " + game.Publisher +
						  "\n Release: " + game.ReleaseDate +
						  "\n Genres: " + game.Genres[0] + ", " + game.Genres[1] + ", " + game.Genres[2] + ", " + game.Genres[3] + ", " + game.Genres[4]);
		}
	}
}

