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

    public GameObject GetGameInfo(int GameID)
    {
		List<string> genres = new List<string>();

		var gamePublishers =
            (from Game in _context.Game
             join Publisher in _context.Publisher
                     on Game.PublisherID equals Publisher.ID
             select new { GameName = Game.Name, GameID = Game.ID, PublisherName = Publisher.Name, GameRelease = Game.ReleaseDate }).ToList();

        var genresList =
            (from GameGenres in _context.GameGenres
             join Game in _context.Game
                     on GameGenres.GameID equals Game.ID
             join Genre in _context.Genre
                     on GameGenres.GenreID equals Genre.ID
             select new { GenreName = Genre.Name, GameID = Game.ID });

        var getGame = gamePublishers.SingleOrDefault(g => g.GameID == GameID);
        foreach (var genre in genresList)
        {
            if (genre.GameID == GameID)
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