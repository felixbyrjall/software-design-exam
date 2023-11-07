using DB;
using DigitalGameStore.DB;

namespace DigitalGameStore.InterestList
{

    internal class AddGame
    {

        public void Add(int gameId)
        {

            Interest newInterest = new()
            {
                GameID = gameId
            };

            using Context database = new Context();
            database.Interest.Add(newInterest);
            database.SaveChanges();
        }
    }
}
