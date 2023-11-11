using DigitalGameStore.Model;
using DigitalGameStore.Controller;

namespace DigitalGameStore.Interfaces;

public class GameRepo : IGameRepo
{
    private readonly Context _context;

	public GameRepo(Context context)
    {
        _context = context;
    }
    public IList<Game> GetGamesOnPage(int start, int end)
    {
		return _context.Game.Where(g => g.ID >= start && g.ID <= end).ToList();
	}

	public GameObject GetGameInfo(int GameID)
	{
		using var context = new Context();
		var gamePublishers =
			(from Game in context.Game
			 join Publisher in context.Publisher
					 on Game.PublisherID equals Publisher.ID
			 select new { GameName = Game.Name, GameID = Game.ID, PublisherName = Publisher.Name, GameRelease = Game.ReleaseDate, Score = Game.Score }).ToList();

		var genresList =
			(from GameGenres in context.GameGenres
			 join Game in context.Game
					 on GameGenres.GameID equals Game.ID
			 join Genre in context.Genre
					 on GameGenres.GenreID equals Genre.ID
			 select new { GenreName = Genre.Name, GameID = Game.ID });
		List<String> genreInfo = new List<string>();

		var getGame = gamePublishers.SingleOrDefault(g => g.GameID == GameID);
		foreach (var genre in genresList)
		{

			if (genre.GameID == GameID)
			{
				genreInfo.Add(genre.GenreName);
			}
		}

		string genresString = genreInfo[0] + ", " + genreInfo[1] + ", " + genreInfo[2] + ", " + genreInfo[3] +
							  ", " + genreInfo[4];

		GameObject gameObjects = new GameObject();
		gameObjects.Name = getGame.GameName;
		gameObjects.Publisher = getGame.PublisherName;
		gameObjects.ReleaseDate = getGame.GameRelease;
		gameObjects.Genres = genresString;

		return gameObjects;
	}
}

