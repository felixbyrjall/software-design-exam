using System;
using NextGaming.Model;

namespace NextGaming.Tools
{
	public class GameInfoView
	{
        public string ShowGameDetails(GameObject game)
        {
            string gameInfo = "Title: " + game.Name +
                          "\n\nPublisher:    " + game.Publisher +
                          "\nRelease date: " + game.ReleaseDate + " (YMD)" +
                          "\nGenres/tags:  " + game.Genres[0] + ", " + game.Genres[1] + ", " + game.Genres[2] + ", " + game.Genres[3] + ", " + game.Genres[4] + "\n";

            return gameInfo;
        }
    }
}