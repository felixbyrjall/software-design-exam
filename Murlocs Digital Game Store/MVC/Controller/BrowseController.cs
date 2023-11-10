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
		/*public string Publisher;
		public string ReleaseDate;
		public string Genre;*/

		public GameItem(int id, string name) //, string publisher, string releaseDate, string genre 
		{
			ID = id;
			Name = name;
			/*Publisher = publisher;
			ReleaseDate = releaseDate;
			Genre = genre;*/
		}
	}

    public class BrowseController
    {
        private List<String> _gamesOnPage = new();
		private List<GameItem> _allGames = new();

		private int _currentPage = 10;
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

		public int GetCurrentPage()
		{
			return _currentPage;
		}

		private void SetCurrentPage(int currentPage)
		{
			_currentPage = currentPage;
		}

		public void Check(int i)
		{
			if (i == 1 && GetCurrentPage() != _lastPage)
			{
				int j = GetCurrentPage();
				SetCurrentPage(j += 10);
			}
			else if (i == 2 && GetCurrentPage() != _firstPage)
			{
				int j = GetCurrentPage();
				SetCurrentPage(j -= 10);
			}
			ListGames();
		}

		public void ListGames()
		{
			if (b == false)
			{
				LoadingScreen();
				b = true;
			}
			var games = _gameRepo.GetGamesOnPage((GetCurrentPage() - 9), GetCurrentPage());
			_gamesOnPage.Clear();
			GetGamesOnPageWithOptions();
			AddGamesToMenu(games); //Kaller på metoden AddGames for å legge til spill i _allGames feltet i view.
		}

		public List<string> GetGamesOnPageWithOptions()
		{
			List<string> options = new List<string> {"Back to main menu", "Next page", "Previous page"};
			options.AddRange(_gamesOnPage);
			return options;
		}

		private void AddGamesToMenu(IEnumerable<Game> games)
        {
            foreach (var game in games)
			{
				GameItem gameitem = new GameItem(game.ID, game.Name);
				_allGames.Add(gameitem.Name);
			}
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
