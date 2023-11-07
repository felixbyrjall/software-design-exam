using DB;
using DigitalGameStore.DB;
using DigitalGameStore.InterestList;
using DigitalGameStore.UI;

namespace DigitalGameStore.Browse;

public class GameInfo
{

    public void ShowGame(int gameId)
    {

        List<String> gameSelections = new List<String>();
        gameSelections.Add("Previous Menu");
        gameSelections.Add("Add to Interest List");

        using Context database = new();

        IList<Publisher> publishers = database.Publisher.ToList();
        IList<Game> games = database.Game.ToList();
        IList<GameGenres> gameGenres = database.GameGenres.ToList();
        IList<Genre> genres = database.Genre.ToList();

        var gamePublishers =
            (from Game in games
             join Publisher in publishers
                 on Game.PublisherID equals Publisher.ID
             select new { GameName = Game.Name, GameID = Game.ID, PublisherName = Publisher.Name, GameRelease = Game.ReleaseDate }).ToList();

        var genresList =
            (from GameGenres in gameGenres
             join Game in games
                 on GameGenres.GameID equals Game.ID
             join Genre in genres
                 on GameGenres.GenreID equals Genre.ID
             select new { GenreName = Genre.Name, GameID = Game.ID });
        List<String> genreInfo = new List<string>();

        var gameRow = gamePublishers.SingleOrDefault(g => g.GameID == gameId);
        foreach (var genre in genresList)
        {

            if (genre.GameID == gameId)
            {
                genreInfo.Add(genre.GenreName);
            }
        }

        gameSelections.Add("Name: " + gameRow.GameName +
                           "\n Publisher: " + gameRow.PublisherName +
                           "\n Release: " + gameRow.GameRelease +
                           "\n Genres: " + genreInfo[0] + ", " + genreInfo[1] + ", " + genreInfo[2] + ", " + genreInfo[3] + ", " + genreInfo[4]);

        string prompt = "(Use the arrows to select an option)";
        string[] options = gameSelections.ToArray();
        MenuLogic mainMenu = new MenuLogic(prompt, options);
        AddGame addGame = new AddGame();

        int selectedIndex = mainMenu.Start();

        switch (selectedIndex)
        {

            case 0:

                break;
            case 1:
                addGame.Add(gameId);
                break;
        }
    }
}