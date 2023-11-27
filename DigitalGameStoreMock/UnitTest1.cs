using NextGaming.Model;
using Moq;
using NextGaming.Interfaces;
using NextGaming.Repo;

namespace NextGamingTest;

public class Tests {
    
    [Test]
    public void Test_GetGameInfoMock()
    {
        // Arrange
        int gameId = 1;
        var mockGameRepo = new Mock<IGameRepo>();

        GameObject expectedGame = new()
        {
            Name = "Counter-Strike 2",
            Publisher = "Valve",
            ReleaseDate = "2012-08-21",
            Genres = new List<string> { "FPS", "Shooter", "Multiplayer", "Competitive", "Action" }
        };
        mockGameRepo.Setup(r => r.GetGameInfo(gameId)).Returns(expectedGame);
        
        // Act
        GameObject gameInfo = mockGameRepo.Object.GetGameInfo(gameId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(gameInfo.Name, Is.EqualTo(expectedGame.Name));
            Assert.That(gameInfo.ReleaseDate, Is.EqualTo(expectedGame.ReleaseDate));
            Assert.That(gameInfo.Publisher, Is.EqualTo(expectedGame.Publisher));
            Assert.That(gameInfo.Genres, Is.EqualTo(expectedGame.Genres).AsCollection);
        });
    }
    [Test]
    public void Test_GetNotInterestedGames_AddGameToInterest_Mock()
    {
        // Arrange
        var mockInterestRepo = new Mock<IInterestRepo>();
        var newInterest = new Interest { GameID = 3 };

        mockInterestRepo.Setup(r => r.AddGameToInterest(newInterest.GameID));

        mockInterestRepo.Setup(r => r.GetNotInterestedGames()).Returns(new List<GameObject>
        {
            new GameObject
            {
                ID = 1,
                Name = "Counter-Strike 2",
                Publisher = "Valve",
                ReleaseDate = "2012-08-21",
                Genres = new List<string>{"FPS", "Shooter", "Multiplayer", "Competitive", "Action"},

            },
            new GameObject
            {
                ID = 2,
                Name = "Dota 2",
                Publisher = "Valve",
                ReleaseDate = "2013-07-09",
                Genres = new List<string>{"MOBA", "Multiplayer", "Strategy", "PVP", "Team-Based"},

            } 
        });

        // Act
        mockInterestRepo.Object.AddGameToInterest(newInterest.GameID);
        var notInterestedList = mockInterestRepo.Object.GetNotInterestedGames();

        // Assert
        foreach (var item in notInterestedList)
        {
            Console.WriteLine(item.Name);
            Assert.That(item.ID, Is.Not.EqualTo(newInterest.GameID));
        }
    }

}