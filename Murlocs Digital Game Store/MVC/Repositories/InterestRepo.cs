using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;

namespace DigitalGameStore.Repo;

public class InterestRepo : IInterestRepo {


    private readonly Context _context;
    private readonly GameObject _gameObject;

    public InterestRepo(Context context, GameObject gameObject) {
        _context = context;
        _gameObject = gameObject;
    }

    public List<GameObject> GetNotInterestedGames(int start, int end)
    {
        var notInterestedList =
            (from Game in _context.Game
             from Interest in _context.Interest.Where(mapping => mapping.GameID == Game.ID).DefaultIfEmpty()
             where Interest.ID == null && Game.ID >= start
             select new { GameName = Game.Name, GameID = Game.ID });
        List<GameObject> list = new List<GameObject>();


        foreach (var item in notInterestedList.Take(10))
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

        GameObject gameObjects = new GameObject(getGame.GameID, getGame.GameName, getGame.PublisherName, getGame.GameRelease, genresString);

        return gameObjects;
    }

}