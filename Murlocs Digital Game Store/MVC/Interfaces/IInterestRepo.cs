using DigitalGameStore.Model;
using DigitalGameStore.Repo;
using Microsoft.EntityFrameworkCore;

namespace DigitalGameStore.Interfaces
{
    public interface IInterestRepo
    {
        public List<GameObject> GetNotInterestedGames(int page);

        public void AddGameToInterest(int gameId);

        public void RemoveGameFromInterest(int gameId);

        public int CountGamesNotInInterestList();


    }
}