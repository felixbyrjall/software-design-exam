using DigitalGameStore.Model;
using DigitalGameStore.Repo;

namespace DigitalGameStore.Interfaces
{
	public interface IInterestRepo {

        public List<GameObject> GetNotInterestedGames(int start, int end);

        public GameObject GetGameInfo(int gameId);

        public void AddGameToInterest(int gameId);

        public void RemoveGameFromInterest(int gameId);
    }
}

