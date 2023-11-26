using NextGaming.Model;

namespace NextGaming.Interfaces {
    public interface IGameRepo
    {
        IList<Game> GetGamesOnPage(int start, int end);

        public GameObject GetGameInfo(int GameID);

        public int CountAllGames();

    }
}
