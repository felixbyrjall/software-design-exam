using DigitalGameStore.Model;

namespace DigitalGameStore.Interfaces
{
    public interface IGameRepo
    {
        IList<Game> GetAllGames(int start, int end);

        public GameObject GetGameInfo(int GameID);

        public List<InterestObject> GetNotInterestedGames(int start, int end);

        public void AddGameToInterest(int gameId);
        
        public void RemoveGameFromInterest(int gameId);

    }
}
