using DigitalGameStore.Views;
using DigitalGameStore.Repo;
using DigitalGameStore.Model;
using DigitalGameStore.Interfaces;

namespace DigitalGameStore.Controller
{
    public class BrowseController
    {
        private List<String> _allGames = new();

		public static int _currentPage = 10; 
		private int _lastPage = 100;
		private int _firstPage = 10;
		private bool b = false;

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
			options.AddRange(_allGames);
			return options;
		}

		private void AddGames(IEnumerable<Game> games)
        {
            foreach (var game in games)
			{
				_allGames.Add("ID: " + game.ID + " Name: " + game.Name);

			}
        }

        public void ListGames()
        {
			if (b == false) {
				_browseView.LoadingScreen();
				b = true;
			}
            var games = _gameRepo.GetAllGames((_currentPage - 9), _currentPage);
            _allGames.Clear();
			GetAllGamesWithOptions();
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

		public void GetSelectedGame(int GameID)
		{
			var game = _gameRepo.GetGameInfo(GameID);
			_browseView.ShowGame(game);
		}

		public void LoadingScreen()
		{
			_browseView.LoadingScreen();
		}
	}
}
