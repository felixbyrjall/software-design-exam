using System.Collections;
using DigitalGameStore.Model;
namespace DigitalGameStore.Interfaces;

public class GameObject
{
	public int ID;
	public string Name;
	public string Publisher;
	public string Release;
	public string Genres;
	
}

public class InterestObject
{
	public int ID;
	public string Name;

	public InterestObject(int id, string name)
	{
		ID = id;
		Name = name;
	}
}

public class GameRepo : IGameRepo
{
    private readonly Context _context;

    public GameRepo(Context context)
    {
        _context = context;
    }
    public IList<Game> GetAllGames(int start, int end)
    {
        return _context.Game.Where(g => g.ID >= start && g.ID <= end).ToList();
	}

    public List<InterestObject> GetNotInterestedGames(int start, int end)
    {
	    var notInterestedList =
		    (from Game in _context.Game
			    from Interest in _context.Interest.Where(mapping => mapping.GameID == Game.ID).DefaultIfEmpty()
				    where Interest.ID == null && Game.ID >= start && Game.ID <= end
			    select new { GameName = Game.Name, GameID = Game.ID });
	    List<InterestObject> list = new List<InterestObject>();
	    
	    
	    foreach (var item in notInterestedList)
	    {
		    InterestObject interestObject = new InterestObject(item.GameID, item.GameName);
		    list.Add(interestObject);
	    }

	    return list;
    }

	public GameObject GetGameInfo(int gameId)
	{
		using var context = new Context();
		var gamePublishers =
			(from Game in context.Game
			 join Publisher in context.Publisher
					 on Game.PublisherID equals Publisher.ID
			 select new { GameName = Game.Name, GameID = Game.ID, PublisherName = Publisher.Name, GameRelease = Game.ReleaseDate }).ToList();

		var genresList =
			(from GameGenres in context.GameGenres
			 join Game in context.Game
					 on GameGenres.GameID equals Game.ID
			 join Genre in context.Genre
					 on GameGenres.GenreID equals Genre.ID
			 select new { GenreName = Genre.Name, GameID = Game.ID});
		List<String> genreInfo = new List<string>();

		var getGame = gamePublishers.SingleOrDefault(g => g.GameID == gameId);
		foreach (var genre in genresList)
		{

			if (genre.GameID == gameId)
			{
				genreInfo.Add(genre.GenreName);
			}
		}

		string genresString = genreInfo[0] + ", " + genreInfo[1] + ", " + genreInfo[2] + ", " + genreInfo[3] +
							  ", " + genreInfo[4];

		GameObject gameObjects = new GameObject();
		gameObjects.Name = getGame.GameName;
		gameObjects.Publisher = getGame.PublisherName;
		gameObjects.Release = getGame.GameRelease;
		gameObjects.Genres = genresString;

		return gameObjects;
	}

	public void AddGameToInterest(int gameId)
	{
		Interest newInterest = new()
		{
			GameID = gameId
		};

		_context.Interest.Add(newInterest);
		_context.SaveChanges();
	}

	public void RemoveGameFromInterest(int gameId)
	{
		Interest removeInterest = new()
		{
			GameID = gameId
		};
		
		_context.Interest.Remove(removeInterest);
		_context.SaveChanges();
	}
}

