using NextGaming.Model;
using Microsoft.EntityFrameworkCore;

public class BrowseGamesTests {
    [Test]
    public void CheckGenres() {
        int[] retrieveGameId = {1}; //the game ids of how many games you want to check
        int[] expectedGenreId = {1,2,3,4,5}; // fill inn the expected genreIds here, there is always 5 ids or empty list if check with query
        using (var db = new Context()) {
            foreach (var gameId in retrieveGameId) {
                var retrievedGameGenres = (
                    from gg in db.GameGenres
                    join game in db.Game on gg.GameID equals game.ID
                    join genre in db.Genre on gg.GenreID equals genre.ID
                    where gg.GameID == gameId
                    select new
                    {
                        GameGenres = gg,
                        Game = game,
                        Genre = genre
                    }
                ).ToList();
                Assert.Multiple(() =>
                {
                    foreach (var ggInfo in retrievedGameGenres) {
                        var genreIdGg = ggInfo.GameGenres.GenreID;
                        var genreId = ggInfo.Genre.ID;

                        if (expectedGenreId.Length == 0) {
                            Assert.That(genreId, Is.EqualTo(genreIdGg)); //assert to check with only the query
                        }
                        else {
                            Assert.That(expectedGenreId, Contains.Item(genreIdGg)); //assert to check for specified genre ids
                        }
                        
                    }
                });
            }
        }
    }

    [Test]
    public void CheckInfo() {
        int retrieveGameId = 1;
        string expectedName = "Counter-Strike 2";
        string expectedRelease = "2012-08-21";
        int expectedPublisher = 1;
        using (var db = new Context()) {
            var retrievedGame = db.Game.FirstOrDefault(g => g.ID == retrieveGameId);
            Assert.Multiple(() =>
            {
                Assert.That(expectedName, Is.EqualTo(retrievedGame.Name));
                Assert.That(expectedPublisher, Is.EqualTo(retrievedGame.PublisherID));
                Assert.That(expectedRelease, Is.EqualTo(retrievedGame.ReleaseDate));
            });
        }
    }
}