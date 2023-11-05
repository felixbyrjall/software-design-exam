using DB;

namespace DigitalGameStoreTest;

public class Tests {
    [SetUp]
    public void Setup()
    {
        using Context db = new();
        db.RemoveRange(db.Game);
        db.SaveChanges();
    }

    [Test]
    public void Test1() {
        using Context db = new();
        string expectedName = "Fredrik";
        db.Game.Add(new()
            { Game_Id = 1, Name = "Fredrik", ReleaseDate = "noe", Score = 0, PublisherID = 1 });
       // db.Product.Add(new() { Product_Id = 1, Name = "Halo4", Price = 499, PublisherID = 1, Date = "22.10.2001" });
        db.SaveChanges();

       string actualName = db.Game.Single().Name;
       int actualID = db.Game.Single().Game_Id;

        Assert.That(actualName, Is.EqualTo(expectedName));
    }
}