using DB;
using DigitalGameStore.DB;

namespace DigitalGameStore.InterestList;

public class DeleteGame {

    public void Delete(int gameId) {

        try
        {
            using Context database = new Context();

            var gameRef = database.Interest.SingleOrDefault(g => g.GameID == gameId);
            if (gameRef != null)
            {
                database.Interest.Remove(gameRef);
                database.SaveChanges();
            }
        }

        catch (Exception e) {

            Console.WriteLine($"Exception: {e.Message}");
        }
    }
}
