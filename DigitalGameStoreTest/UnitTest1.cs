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
            { Username = "Fredrik", Email = "noe", Password = "Noe", Type = "noe", MurlocCoins = 100 });
        db.SaveChanges();

       string actualName = db.Users.Single().Username;
       int actualID = db.Users.Single().UserID;

        Assert.That(actualName, Is.EqualTo(expectedName));
        Assert.That(actualID, Is.EqualTo(2));
    }
}