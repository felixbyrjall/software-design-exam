using NextGaming.Model;
using NextGaming.Repo;

namespace NextGamingIntegrationTest; 

public class GameRepoTests {
    private readonly GameRepo _gameRepo = new(new Context());

    [Test]
    public void Test_GetGamesOnPage() {
        //Arrange
        int startPage = 15;
        int endPage = 100;
        int expectedCount = endPage - startPage + 1;
        //Act
        var allGamesOnPage = _gameRepo.GetGamesOnPage(startPage, endPage);
        foreach (var item in allGamesOnPage) {
            //Assert
            Assert.That(allGamesOnPage.Count, Is.EqualTo(expectedCount));
        }
    }

    [Test]
    public void Test_GetGameInfo() {
        
        //Arrange
        int gameId = 1;
        var expectedGame = new GameObject()
        {
            Name = "Counter-Strike 2",
            Publisher = "Valve",
            ReleaseDate = "2012-08-21",
            Genres = new List<string>{"FPS", "Shooter", "Multiplayer", "Competitive", "Action"},
        };
        
        //Act
        var gameInfo = _gameRepo.GetGameInfo(gameId);
        
        //Assert
       Assert.Multiple(() =>
        {
            Assert.That(gameInfo.Name, Is.EqualTo(expectedGame.Name));
            Assert.That(gameInfo.ReleaseDate, Is.EqualTo(expectedGame.ReleaseDate));
            Assert.That(gameInfo.Publisher, Is.EqualTo(expectedGame.Publisher));
            Assert.That(gameInfo.Genres, Is.EqualTo(expectedGame.Genres).AsCollection);

        });
    }

    [Test]
    public void Test_CountAllGames() {
        //Act
       int gameCount = _gameRepo.CountAllGames();
       //Assert
       Assert.That(gameCount, Is.EqualTo(100));
    }
}