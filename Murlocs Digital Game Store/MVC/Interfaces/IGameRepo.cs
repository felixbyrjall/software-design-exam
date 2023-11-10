using DigitalGameStore.Model;

namespace DigitalGameStore.Interfaces
{
    public interface IGameRepo
    {
        IList<Game> GetAllGames(int start, int end);

        public GameObject GetGameInfo(int GameID);

	}
}
