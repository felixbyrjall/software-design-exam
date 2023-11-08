using DB;
using DigitalGameStore.DB;
using System.Collections.Generic;
using System.Linq;

namespace DigitalGameStore.Models
{
    public class GameModel
    {
        public IList<Game> GetAllGames(int page, int pageSize)
        {
            using var context = new Context();
            return context.Game
                          .OrderBy(g => g.ID)
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .ToList();
        }

        public Game GetGameDetails(int gameId)
        {
            using var context = new Context();
            var game = context.Game
                             .SingleOrDefault(g => g.ID == gameId);

            if (game != null)
            {
                context.Entry(game).Reference(g => g.Publisher).Load();

            }

            return game;
        }

    }
}
