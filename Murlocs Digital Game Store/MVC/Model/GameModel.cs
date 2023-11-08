using DigitalGameStore.DB;
namespace DigitalGameStore.Model;

public class GameModel
    {
        public IList<Game> GetAllGames()
        {
            using var context = new Context();
            return context.Game.ToList();
        }
    }
