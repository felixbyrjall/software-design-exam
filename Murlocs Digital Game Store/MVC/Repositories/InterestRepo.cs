using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;

namespace DigitalGameStore.Repo;

public class InterestObject {
    public int GameID;
    public string Name;
    public string Publisher;
    public string Release;
    public string Genres;
}

public class InterestRepo : IInterestRepo {


    private readonly Context _context;

    public InterestRepo(Context context) {
        _context = context;
    }

    public IList<Interest> GetAllInterest() {
        return _context.Interest.ToList();
    }
    public InterestObject GetInterestInfo(int GameID) {
        using var context = new Context();
        var gameInfo = (from Game in context.Game
            join Interest in context.Interest on Game.ID equals Interest.GameID
            join Publisher in context.Publisher on Game.PublisherID equals Publisher.ID
            select new
            {
                GameName = Game.Name, GameID = Game.ID, PublisherName = Publisher.Name, ReleaseDate = Game.ReleaseDate
            });

        var genresList =
            (from GameGenres in context.GameGenres
                join Game in context.Game
                    on GameGenres.GameID equals Game.ID
                join Genre in context.Genre
                    on GameGenres.GenreID equals Genre.ID
                select new { GenreName = Genre.Name, GameID = Game.ID });
        List<String> genreInfo = new List<string>();

        var getGame = gameInfo.SingleOrDefault(g => g.GameID == GameID);
        foreach (var genre in genresList) {

            if (genre.GameID == GameID) {
                genreInfo.Add(genre.GenreName);
            }
        }
        string genresString = genreInfo[0] + ", " + genreInfo[1] + ", " + genreInfo[2] + ", " + genreInfo[3] +
                              ", " + genreInfo[4];

        InterestObject interestObject = new();
        interestObject.GameID = getGame.GameID;
        interestObject.Name = getGame.PublisherName;
        



        return interestObject;
    }

    public void AddInterest(int GameID) {
        using (Context db = new Context()) {
            db.Interest.Add(new Interest()
            {
                GameID = GameID
            });
            db.SaveChanges();
        }



    }
}


