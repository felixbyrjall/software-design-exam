using NextGaming.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using NextGaming.Interfaces;
using NextGaming.Repo;

namespace NextGamingTest;

public class Tests {
    
    [Test]
    public void Test_GetGameInfo() {
        
        //Arrange
        var mockGameRepo = new Mock<IGameRepo>();
        int gameId = 4;
        var expectedGame = new GameObject()
        {
            Name = "Counter-Strike 2",
            Publisher = "Valve",
            ReleaseDate = "2012-08-21",
            Genres = new List<string>{"FPS", "Shooter", "Multiplayer", "Competitive", "Action"},
        };
        mockGameRepo.Setup(r => r.GetGameInfo(gameId)).Returns(expectedGame);
        //Act
        var gameInfo = mockGameRepo.Object.GetGameInfo(gameId);
        
        //Assert
        Assert.Multiple(() =>
        {
            Console.WriteLine(gameInfo.Name);
            Assert.That(gameInfo.Name, Is.EqualTo(expectedGame.Name));
            Assert.That(gameInfo.ReleaseDate, Is.EqualTo(expectedGame.ReleaseDate));
            Assert.That(gameInfo.Publisher, Is.EqualTo(expectedGame.Publisher));
            Assert.That(gameInfo.Genres, Is.EqualTo(expectedGame.Genres).AsCollection);

        });
    }
    
    [OneTimeSetUp]
    public void OneTimeSetup() {
    }
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void AddGameTest()
    {
        int expectedId = 1;
        string expectedName = "Counter Strike 2";
        string expectedRelease = "21.08.2012";
        int expectedScore = 0;
        int expectedPublisher = 1;
        
        using (var db = new Context()) {
            var publisher = new Publisher { ID = expectedPublisher, Name = "Valve" };
            db.Publisher.Add(publisher);
            db.SaveChanges();
            
            var addedGame = new Game
            { ID = expectedId, Name = expectedName, ReleaseDate = expectedRelease, Score = expectedScore, PublisherID = expectedPublisher };
            db.Game.Add(addedGame);
            db.SaveChanges();
        
            var retrievedGame = db.Game.Single();
        
            Assert.AreEqual(expectedId, retrievedGame.ID);
            Assert.AreEqual(expectedName, retrievedGame.Name);
            Assert.AreEqual(expectedRelease, retrievedGame.ReleaseDate);
            Assert.AreEqual(expectedScore, retrievedGame.Score);
            Assert.AreEqual(expectedPublisher, retrievedGame.PublisherID);
        }
    }

    [Test]
    public void AddInterestTest() {
        int expectedId = 1;
        int expectedGame = 1;
        
        using (var db = new Context()) {
            var publisher = new Publisher { ID = 1, Name = "Valve" };
            db.Publisher.Add(publisher);
            var game = new Game { ID = 1, Name = "Counter Strike 2", ReleaseDate = "21.08.2012", Score = 0, PublisherID = 1 };
            db.Game.Add(game);
            db.SaveChanges();
            
            var addedInterest = new Interest { ID = expectedId, GameID = expectedGame };
            db.Interest.Add(addedInterest);
            db.SaveChanges();
            
            var retrievedInterest = db.Interest.Single();
            Assert.AreEqual(expectedId, retrievedInterest.ID);
            Assert.AreEqual(expectedGame, retrievedInterest.GameID);
        }
    }

    [Test]
    public void DeleteInterestTest() {
        int deleteId = 1; //ID of the game you want to delete
        var interests = new List<Interest>();
        using (var db = new Context()) {
            var publisher = new Publisher { ID = 1, Name = "Valve" };
            db.Publisher.Add(publisher); //Adding a publisher so the foreign key works

            var game1 = new Game { ID = 1, Name = "Counter Strike 2", ReleaseDate = "21.08.2012", Score = 0, PublisherID = 1 };
            var game2 = new Game { ID = 2, Name = "Dota 2", ReleaseDate = "09.07.2013", Score = 0, PublisherID = 1 };
            var game3 = new Game { ID = 12, Name = "Team Fortress 2", ReleaseDate = "10.10.2007", Score = 0, PublisherID = 1 };
            db.Game.AddRange(game1, game2, game3);
            
            Interest[] interest =
            {
                new Interest { ID = 1, GameID = 1 },
                new Interest { ID = 2, GameID = 2 },
                new Interest { ID = 3, GameID = 12 }
            };
            db.Interest.AddRange(interest);
            db.SaveChanges();
   
            var removedInterest = db.Interest.FirstOrDefault(i => i.GameID == deleteId);
            if (removedInterest!=null) {
                db.Interest.Remove(removedInterest);
                db.SaveChanges();
            }
   
            int actualCount = db.Interest.Count();
            Assert.AreNotEqual(interests.Count(), actualCount);
        }
    }
    
}