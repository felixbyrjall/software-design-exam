using DB;

namespace DigitalGameStore.InterestList;

public class DeleteGame {

    public void Delete(int gameId) {

        using Context database = new Context();

        var gameRef = database.Interest.Find(gameId);

        if (gameRef != null) {
            
            database.Interest.Remove(gameRef);
            database.SaveChanges();
        }
    }
}
