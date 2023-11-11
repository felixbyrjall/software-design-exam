using DigitalGameStore.Model;

namespace DigitalGameStore.Interfaces
{
    public interface IGameRepo
    {
        IList<Game> GetGamesOnPage(int start, int end);

        public GameObject GetGameInfo(int GameID);

        public int CountAllGames();

    }
}
