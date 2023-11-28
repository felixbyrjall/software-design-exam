using NextGaming.Interfaces;
using NextGaming.Model;

namespace NextGaming.Repo;

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

    public int CountAllGames() // Count all games for scalability
    {
        var allGames = _context.Game.Count();

        return allGames;
    }

    public GameObject GetGameInfo(int gameId)
    {
		List<string> genres = new List<string>();

		var gamePublishers =
            (from game in _context.Game
             join publisher in _context.Publisher
                     on game.PublisherID equals publisher.ID
             select new { GameName = game.Name, GameID = game.ID, PublisherName = publisher.Name, GameRelease = game.ReleaseDate }).ToList();

        var genresList =
            (from gameGenres in _context.GameGenres
             join game in _context.Game
                     on gameGenres.GameID equals game.ID
             join genre in _context.Genre
                     on gameGenres.GenreID equals genre.ID
             select new { GenreName = genre.Name, GameID = game.ID });

        var getGame = gamePublishers.SingleOrDefault(g => g.GameID == gameId);
        foreach (var genre in genresList)
        {
            if (genre.GameID == gameId)
            {
                genres.Add(genre.GenreName);
            }
        }

        GameObject gameObjects = new GameObject();
        gameObjects.Name = getGame.GameName;
        gameObjects.Publisher = getGame.PublisherName;
        gameObjects.ReleaseDate = getGame.GameRelease;
        gameObjects.Genres = genres;

        return gameObjects;
    }
}