using NextGaming.Model;

namespace NextGaming.Interfaces
{
    public interface IGameRepo
    {
        IList<Game> GetGamesOnPage(int start, int end);

        public GameObject GetGameInfo(int gameId);

        public int CountAllGames();
    }
}