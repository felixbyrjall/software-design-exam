using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;

namespace DigitalGameStore.Repo;

public class InterestRepo : IInterestRepo {


    private readonly Context _context;
    private readonly GameObject _gameObject;

    private const int _numberOfGamesOnPage = 10;

    public InterestRepo(Context context, GameObject gameObject) {
        _context = context;
        _gameObject = gameObject;
    }

    public List<GameObject> GetNotInterestedGames(int page)
    {
        var notInterestedList =
            (from Game in _context.Game
             from Interest in _context.Interest.Where(mapping => mapping.GameID == Game.ID).DefaultIfEmpty()
             where Interest.ID == null
             select new { GameName = Game.Name, GameID = Game.ID }).Skip(page - 10).Take(10);
        List<GameObject> list = new List<GameObject>();


        foreach (var item in notInterestedList)
        {
            GameObject gameObject = new GameObject(item.GameID, item.GameName);
            list.Add(gameObject);
        }

        return list;
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
             select new { GenreName = Genre.Name, GameID = Game.ID });
        List<String> genres = new List<string>();

        var getGame = gamePublishers.SingleOrDefault(g => g.GameID == gameId);
        foreach (var genre in genresList)
        {
            if (genre.GameID == gameId)
            {
                genres.Add(genre.GenreName);
            }
        }

        GameObject gameObjects = new GameObject(getGame.GameID, getGame.GameName, getGame.PublisherName, getGame.GameRelease, genres);

        return gameObjects;
    }

}