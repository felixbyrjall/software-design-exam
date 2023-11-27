using NextGaming.Model;
using NextGaming.Repo;

namespace NextGamingTest; 

public class GameGenreRepoTests {
    private readonly GameGenresRepo _gameGenresRepo = new GameGenresRepo(new Context());
    private readonly InterestRepo _interestRepo = new InterestRepo(new Context());

    [SetUp]
    public void Setup()
    {
        using Context db = new();
        db.RemoveRange(db.Interest);
        db.SaveChanges();
    }
    [Test]
    public void Test_RecommendGames() {
        //Arrange
        var newInterest = new Interest
        {
            GameID = 1
        };
        var expectedTopGame = new Game
        {
            ID = 12,
            Name = "Team Fortress 2",
            ReleaseDate = "2007-10-10",
            PublisherID = 1,
            Publisher = new Publisher
            {
                ID = 1,
                Name = "Valve"
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
                 Console.WriteLine(item.ID);
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