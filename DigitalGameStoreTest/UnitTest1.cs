using DB;

namespace DigitalGameStoreTest;

public class Tests {
    [SetUp]
    public void Setup()
    {
        using DigitalGameStoreContext db = new();
        db.RemoveRange(db.Users);
        db.SaveChanges();
    }

    [Test]
    public void Test1() {
        using DigitalGameStoreContext db = new();
        string expectedName = "Fredrik";
        db.Users.Add(new()
            { UserID = 1, Username = "Fredrik", Email = "noe", Password = "Noe", Type = "noe", MurlocCoins = 100 });
       // db.Product.Add(new() { Product_Id = 1, Name = "Halo4", Price = 499, PublisherID = 1, Date = "22.10.2001" });
        db.SaveChanges();

       string actualName = db.Users.Single().Username;

        Assert.That(actualName, Is.EqualTo(expectedName));

    }
}