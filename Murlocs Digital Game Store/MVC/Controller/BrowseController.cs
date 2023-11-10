using System.Collections.Immutable;
using DigitalGameStore.Views;
using DigitalGameStore.Repo;
using DigitalGameStore.Model;
using DigitalGameStore.Interfaces;

namespace DigitalGameStore.Controller
{
	public class GameItem
	{
		public int ID;
		public string Name;

		public GameItem(int id, string name)
		{
			ID = id;
			Name = name;
		}
	}
	
    public class BrowseController
    {
        private List<GameItem> _allGames = new();

		public static int _currentPage = 10; 
		private int _lastPage = 100;
		private int _firstPage = 10;

		private readonly IGameRepo _gameRepo;
		private readonly BrowseView _browseView;

		public BrowseController(IGameRepo gameRepo, BrowseView browseView)
		{
			_gameRepo = gameRepo;
			_browseView = browseView;
		}
		
		public List<string> GetAllGamesWithOptions()
		{
			List<string> options = new List<string> {"Back to main menu", "Next page", "Previous page"};
			foreach (var item in _allGames)
			{
				options.Add("ID: " + item.ID +" Name: " + item.Name);
			}
			return options;
		}

		private void AddGames(IEnumerable<Game> games)
        {
            foreach (var game in games)
            {
	            GameItem gameItem = new GameItem(game.ID, game.Name);
				_allGames.Add(gameItem);
			}
        }
		
		private void AddNotInterested(List<InterestObject> games)
		{
			foreach (var game in games)
			{
				GameItem gameItem = new GameItem(game.ID, game.Name);
				_allGames.Add(gameItem);
			}
		}

        public void ListGames()
        {
            var games = _gameRepo.GetAllGames((_currentPage - 9), _currentPage);
            _allGames.Clear();
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

		public void GetSelectedGame(int gameId)
		{
			var game = _gameRepo.GetGameInfo(_allGames[gameId].ID);
			_browseView.ShowGame(game);
		}

		public void NotInterestedGames(int i)
		{
			if(i == 1 && _currentPage != _lastPage)
			{
				_currentPage += 10;
			}
			else if(i == 2 && _currentPage != _firstPage)
			{
				_currentPage -= 10;
			}
			ListNotInterested();
		}

		public void AddInterest(int gameId)
		{
			_gameRepo.AddGameToInterest(_allGames[gameId].ID);
		}
		
		public void RemoveInterest(int gameId)
		{
			_gameRepo.RemoveGameFromInterest(gameId);
		}
		
		public void ListNotInterested()
		{
			var gamesInterested = _gameRepo.GetNotInterestedGames((_currentPage - 9), _currentPage);
			_allGames.Clear();
			AddNotInterested(gamesInterested); //Kaller på metoden AddGames for å legge til spill i _allGames feltet i view.
		}
	}
}
