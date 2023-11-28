using DigitalGameStore.MVC.Controller;
using NextGaming.Model;
using NextGaming.Repo;

namespace NextGamingIntegrationTest; 

public class InterestTests {
    private readonly Context _context = new();
    private readonly InterestRepo _interestRepo = new(new Context(), new NotificationController());

    [SetUp]
    public void Setup()
    {
        _context.RemoveRange(_context.Interest);
        _context.SaveChanges();
    }
    [Test]
    public void Test_GetNotInterestedGames_AddGameToInterest() {
        //Arrange
        var newInterest = new Interest { GameID = 4 };
        _interestRepo.AddGameToInterest(newInterest.GameID); 
            
        //Act
        var notInterestedList = _interestRepo.GetNotInterestedGames();
            
        //Assert
        foreach (var item in notInterestedList)
        {
            Assert.That(item.ID, Is.Not.EqualTo(newInterest.GameID),
                "Interest list dose´nt contain the new interest");
        }
    }
    [Test]
    public void Test_RemoveGameFromInterest() {
        //Arrange
        var newInterest = new Interest
        {
            GameID = 1
        };
        _interestRepo.AddGameToInterest(newInterest.GameID);
        //Act
        _interestRepo.RemoveGameFromInterest(newInterest.GameID);
        foreach (var item in _interestRepo.GetGamesOnInterestList(1)) {
            //Assert
            Assert.That(item.ID, Does.Not.EqualTo(newInterest.GameID));
        }
    }
    [Test]
    public void Test_CountGamesIn_And_NotInInterestList() {
        //Act
       int gamesInList = _interestRepo.CountGamesInInterestList();
       int gamesNotInList = _interestRepo.CountGamesNotInInterestList();
       //Assert
        Assert.Multiple(() =>
        {
            Assert.That(gamesInList, Is.EqualTo(_interestRepo.GetGamesOnInterestList(1).Count));
            Assert.That(gamesNotInList, Is.EqualTo(_interestRepo.GetNotInterestedGames().Count));

        });
    }
    [Test]
    public void Test_GetGamesOnInterestList() {
        //Arrange
        var newInterest = new Interest
        {
            GameID = 4
        };
        //Act
        _interestRepo.AddGameToInterest(newInterest.GameID);
        foreach (var item in _interestRepo.GetGamesOnInterestList(1)) { 
            
            //Assert
            Assert.That(item.ID, Is.EqualTo(newInterest.GameID));
        }
        
    }
    
}