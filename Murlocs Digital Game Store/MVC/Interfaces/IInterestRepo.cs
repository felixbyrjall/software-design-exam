using NextGaming.Model;
using NextGaming.Repo;

namespace NextGaming.Interfaces
{
    public interface IInterestRepo
    {
        public List<GameObject> GetNotInterestedGames();

        public void AddGameToInterest(int gameId);

        public void RemoveGameFromInterest(int gameId);

        public int CountGamesNotInInterestList();

        public List<GameObject> GetGamesOnInterestList(int page);

		public int CountGamesInInterestList();
	}
}