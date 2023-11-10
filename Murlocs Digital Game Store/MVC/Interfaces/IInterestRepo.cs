using DigitalGameStore.Model;
using DigitalGameStore.Repo;

namespace DigitalGameStore.Interfaces
{
	public interface IInterestRepo {
		public IList<Interest> GetAllInterest();
		public InterestObject GetInterestInfo(int GameID);

	}
}

