using DigitalGameStore.DB;
namespace DigitalGameStore.Model;

public class GameModel
    {
        public IList<Game> GetAllGames(int start, int end)
        {
            using var context = new Context();
            return context.Game.Where(g => g.ID >= start && g.ID <= end).ToList();
        }
    }
