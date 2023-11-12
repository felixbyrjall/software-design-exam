﻿using DigitalGameStore.Model;

namespace DigitalGameStore.Interfaces {
    public interface IInterestRepo
    {
        public List<GameObject> GetNotInterestedGames(int page);

        public void AddGameToInterest(int gameId);

        public void RemoveGameFromInterest(int gameId);

        public int CountGamesNotInInterestList();

        public List<GameObject> GetGamesOnInterestList(int page);
    }
}