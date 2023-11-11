using DigitalGameStore.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalGameStoreTest;


public class Tests
{
    
    [OneTimeSetUp]
    public void OneTimeSetup() {
        using Context db = new();
        db.Database.EnsureDeleted();
        db.Database.Migrate();
    }
    [SetUp]
    public void Setup()
    {
        using Context db = new();
        db.RemoveRange(db.Game);
        db.RemoveRange(db.Publisher);
        db.SaveChanges();
    }
    
    //Testing based on Arrange, Act, Assert pattern.

    [Test]
    public void AddGameTest()
    {
        int expectedId = 1;
        string expectedName = "Counter Strike 2";
        string expectedRelease = "21.08.2012";
        int expectedScore = 0;
        int expectedPublisher = 1;
        
        using (var db = new Context()) {
            //Arrange
            var publisher = new Publisher { ID = expectedPublisher, Name = "Valve" };
            db.Publisher.Add(publisher);
            db.SaveChanges();
        }
        
        using (var db = new Context()) {
            //Act
            var addedGame = new Game
            { ID = expectedId, Name = expectedName, ReleaseDate = expectedRelease, Score = expectedScore, PublisherID = expectedPublisher };
            db.Game.Add(addedGame);
            db.SaveChanges();
        }
        
        using (var db = new Context()) {
            //Assert
            var retrivedGame = db.Game.Single();
        
            Assert.AreEqual(expectedId, retrivedGame.ID);
            Assert.AreEqual(expectedName, retrivedGame.Name);
            Assert.AreEqual(expectedRelease, retrivedGame.ReleaseDate);
            Assert.AreEqual(expectedScore, retrivedGame.Score);
            Assert.AreEqual(expectedPublisher, retrivedGame.PublisherID);
        }
    }

    [Test]
    public void AddInterestTest() {
        int expectedId = 1;
        int expectedGame = 1;
        
        using (var db = new Context()) {
            //Arrange
            var publisher = new Publisher { ID = 1, Name = "Valve" };
            db.Publisher.Add(publisher);
            var game = new Game { ID = 1, Name = "Counter Strike 2", ReleaseDate = "21.08.2012", Score = 0, PublisherID = 1 };
            db.Game.Add(game);
            db.SaveChanges();
        }
        
        using (var db = new Context()) {
            //Act
            var interest = new Interest { ID = expectedId, GameID = expectedGame };
            db.Interest.Add(interest);
            db.SaveChanges();
        }
        
        using (var db = new Context()) {
            //Assert
            var addedInterest = db.Interest.Single();
            Assert.AreEqual(expectedId, addedInterest.ID);
            Assert.AreEqual(expectedGame, addedInterest.GameID);
        }
    }

    [Test]
    public void DeleteInterestTest() {
        int deleteId = 1; //ID of the game you want to delete
        var interests = new List<Interest>();
        using (var db = new Context()) {
            //Arrange
            var publisher = new Publisher { ID = 1, Name = "Valve" };
            db.Publisher.Add(publisher); //Adding a publisher so the foregin key works

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
        }
        using (var db = new Context()) {
            //Act
            var removeInterest = db.Interest.FirstOrDefault(i => i.GameID == deleteId);
            if (removeInterest!=null) {
                db.Interest.Remove(removeInterest);
                db.SaveChanges();
            }
        }

        using (var db = new Context()) {
            //Assert
            int actualCount = db.Interest.Count();
            Assert.AreNotEqual(interests.Count(), actualCount);
        }
    }
    
}