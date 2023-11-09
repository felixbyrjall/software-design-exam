﻿using DigitalGameStore.Views;
using DigitalGameStore.Repo;
using DigitalGameStore.Model;

namespace DigitalGameStore.Controller
{
    public class BrowseController
    {

        private readonly IGameRepo _gameRepo;
        public BrowseController(IGameRepo gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public static int _currentPage = 10;
		private int _lastPage = 100;
		private int _firstPage = 10;


		private void AddGames(IEnumerable<Game> games)
        {
			foreach (var game in games)
			{
				BrowseView._allGames.Add("ID: " + game.ID + " Name: " + game.Name);
			}
        }

        public void ListGames()
        {
            var games = _gameRepo.GetAllGames((_currentPage - 9), _currentPage); // Henter spill fra GetAllGames metoden inne i GameRepo
            AddGames(games); //Kaller på metoden AddGames for å legge til spill i _allGames feltet i view.
        }

		public void Check(int i)
		{
			if(i == 1 && _currentPage != _lastPage)
			{
				_currentPage += 10;
			}
			else if(i == 2 && _currentPage != _firstPage)
			{
				_currentPage -= 10;
			}
			ListGames();
		}
	}
}
