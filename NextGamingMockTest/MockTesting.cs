using NextGaming.Model;
using Moq;
using NextGaming.Interfaces;

namespace NextGamingMockTest;

public class Tests {
    
    [Test]
    public void Test_GetGameInfoMock()
    {
        // Arrange
        var mockGameRepo = new Mock<IGameRepo>();

        GameObject expectedGame = new()
        {
            ID = 1,
            Name = "Counter-Strike 2",
            Publisher = "Valve",
            ReleaseDate = "2012-08-21",
            Genres = new List<string> { "FPS", "Shooter", "Multiplayer", "Competitive", "Action" }
        };
        mockGameRepo.Setup(r => r.GetGameInfo(expectedGame.ID)).Returns(expectedGame);
        
        // Act
        GameObject gameInfo = mockGameRepo.Object.GetGameInfo(expectedGame.ID);

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
    public void Test_GetNotInterestedGames_Mock()
    {
        // Arrange
        var mockInterestRepo = new Mock<IInterestRepo>();
        var newInterest = new GameObject
        {
            ID = 3,
            Name = "Dota 2",
            Publisher = "Valve",
            ReleaseDate = "2013-07-09",
            Genres = new List<string>{"MOBA", "Multiplayer", "Strategy", "PVP", "Team-Based"},
        };
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
        var notInterestedList = mockInterestRepo.Object.GetNotInterestedGames();

        // Assert
        foreach (var item in notInterestedList)
        {
            Assert.That(item.ID, Is.Not.EqualTo(newInterest.ID));
        }
    }

    [Test]
    public void Test_GetIntGenres_Mock() {
        //Arrange
        var mockGetIntGenres = new Mock<IGameGenreRepo>();
        var expectedGenres = new List<int>
        {
            1,2,3,4,5
        };
        mockGetIntGenres.Setup(g => g.GetIntGenres()).Returns(expectedGenres);
      //Act
       var genres = mockGetIntGenres.Object.GetIntGenres();
       foreach (var item in genres) {
           //Assert
           Assert.That(genres, Is.EqualTo(expectedGenres));
       }
    }
}