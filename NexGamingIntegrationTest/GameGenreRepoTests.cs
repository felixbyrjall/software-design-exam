using DigitalGameStore.MVC.Controller;
using NextGaming.Model;
using NextGaming.Repo;

namespace NextGamingIntegrationTest; 

public class GameGenreRepoTests {
    private readonly GameGenresRepo _gameGenresRepo = new(new Context());
    private readonly InterestRepo _interestRepo = new(new Context(), new NotificationController());
    private readonly Context _context = new();
    
    [SetUp]
    public void Setup()
    {
        _context.RemoveRange(_context.Interest);
        _context.SaveChanges();
    }
    [Test]
    public void Test_RecommendGames_WithOneGame() {
        //Arrange
        var newInterest = new Interest
        {
            GameID = 28
        };
        var expectedTopGame = new Game
        {
            ID = 20,
            Name = "Don't Starve Together",
            ReleaseDate = "2016-04-21",
            PublisherID = 18,
            Publisher = new Publisher
            {
                ID = 18,
                Name = "Klei Entertainment"
            }
        };
        _interestRepo.AddGameToInterest(newInterest.GameID);
       var expectedScore = _gameGenresRepo.ScoreGame(expectedTopGame);
       
        //Act
        var recommendedGames = _gameGenresRepo.RecommendGames();
        
         foreach (var item in recommendedGames.GetRange(0,1)) {
             
             //Assert
             Assert.Multiple(() =>
             {
                 Assert.That(item.Score, Is.EqualTo(expectedScore)); //Checks that the top game has the same score
                 Assert.That(item.ID, Is.EqualTo(expectedTopGame.ID)); //Checks that the top game has the same ID
             });
        }
    }
    
    [Test]
    public void Test_RecommendGames_WithTwoGames() {
        //Arrange
        var newInterest1 = new Interest
        {
            GameID = 30
        };
        var newInterest2 = new Interest
        {
            GameID = 45
        };
        var expectedTopGame = new Game
        {
            ID = 51,
            Name = "Garry's Mod",
            ReleaseDate = "2006-11-29",
            PublisherID = 1,
            Publisher = new Publisher
            {
                ID = 1,
                Name = "Valve"
            }
        };
        _interestRepo.AddGameToInterest(newInterest1.GameID);
        _interestRepo.AddGameToInterest(newInterest2.GameID);
        var expectedScore = _gameGenresRepo.ScoreGame(expectedTopGame);
       
        //Act
        var recommendedGames = _gameGenresRepo.RecommendGames();
        
        foreach (var item in recommendedGames.GetRange(0,1)) {
             
            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(item.Score, Is.EqualTo(expectedScore)); //Checks that the top game has the same score
                Assert.That(item.ID, Is.EqualTo(expectedTopGame.ID)); //Checks that the top game has the same ID
            });
        }
    }
    
    [Test]
    public void Test_GetIntGenres() {
        int[] expectedGenres = { 1, 2, 3, 4, 5 }; //Genre ids for game id 1
        var newInterest = new Interest
        {
            GameID = 1
        };
        _interestRepo.AddGameToInterest(newInterest.GameID);
       var getGenres = _gameGenresRepo.GetIntGenres();
       List<int> actuallGenres = new List<int>();
       foreach (var item in getGenres) {
           actuallGenres.Add(item);
       }
       Assert.That(actuallGenres, Is.EqualTo(expectedGenres));
    }
}