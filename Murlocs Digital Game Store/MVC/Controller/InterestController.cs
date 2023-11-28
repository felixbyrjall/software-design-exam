using DigitalGameStore.MVC.Controller;
using NextGaming.Interfaces;
using NextGaming.Model;
using NextGaming.Repo;
using NextGaming.Tools;
using NextGaming.Views;

namespace NextGaming.Controller;

public class InterestController
{
	#region Fields and dependencies
	private const int _firstPage = 10;
	private int _currentPage = 10;
	private int _lastPageOnAddToInterestList;
	private int _numberOfGamesInList;
	public static int CurrentIndex;
	private string _notification = "";
	private readonly IInterestRepo _interestRepo;
	private readonly IGameRepo _gameRepo;
	private readonly MenuLogic _menuLogic;
	private readonly GameInfoView _gameInfoView;

	private List<GameObject> _gamesNotInInterestListOnCurrentPage = new();
	private List<GameObject> _allGamesNotInInterestList = new();
	private List<GameObject> _gamesInInterestListOnCurrentPage = new();

	public InterestController(IInterestRepo interestRepo, IGameRepo gameRepo, MenuLogic menuLogic, GameInfoView gameInfoView)
	{
		_interestRepo = interestRepo;
		_gameRepo = gameRepo;
		_menuLogic = menuLogic;
		_gameInfoView = gameInfoView;
	}
	#endregion

	#region Getter and setter for _currentPage
	public int GetCurrentPage()
	{
		return _currentPage;
	}

	public void SetCurrentPage(int currentPage)
	{
		_currentPage = currentPage;
	}
	#endregion

	#region List games not on interest list on current page
	private void GamesNotAddedToListOnCurrentPage(List<GameObject> games)
	{
		foreach (var game in games)
		{
			GameObject gameObject = new GameObject(game.ID, game.Name);
			_gamesNotInInterestListOnCurrentPage.Add(gameObject);
		}
	}

	public List<GameObject> NotInterestedGamesOnCurrentPage()
	{
		var listFromRepo = _interestRepo.GetNotInterestedGames().Skip(GetCurrentPage() - 10).Take(10);
		List<GameObject> list = new List<GameObject>();

		foreach (var item in listFromRepo)
		{
			GameObject gameObject = new GameObject(item.ID, item.Name);
			list.Add(gameObject);
		}
		return list;
	}

	public void ListNotInterestedOnCurrentPage()
	{
		var gamesInterested = NotInterestedGamesOnCurrentPage();
		_gamesNotInInterestListOnCurrentPage.Clear();
		GamesNotAddedToListOnCurrentPage(gamesInterested);
	}

	public void CheckCurrentPageAndDisplayGamesNotOnInterestList(int i)
	{
		if (i == 1 && GetCurrentPage() < _lastPageOnAddToInterestList)
		{
			int j = GetCurrentPage();
			SetCurrentPage(j += 10);
		}
		else if (i == 2 && GetCurrentPage() != _firstPage)
		{
			int j = GetCurrentPage();
			SetCurrentPage(j -= 10);
		}
		ListNotInterestedOnCurrentPage();
	}
	#endregion

	#region List ALL games not on interest list
	public void ListAllNotInterested()
	{
		var gamesInterested = _interestRepo.GetNotInterestedGames();
		_allGamesNotInInterestList.Clear();
		AllGamesNotAddedToList(gamesInterested);
	}

	private void AllGamesNotAddedToList(List<GameObject> games)
	{
		foreach (var game in games)
		{
			GameObject gameObject = new GameObject(game.ID, game.Name);
			_allGamesNotInInterestList.Add(gameObject);
		}
	}
	#endregion

	#region Menu logic
	public List<string> GetGamesOnPageWithOptions()
	{
		_lastPageOnAddToInterestList = _interestRepo.CountGamesNotInInterestList();
		List<string> options = new List<string> { "Back to interest list", "Next page", "Previous page", "---- Games remaining: " + _lastPageOnAddToInterestList + " -----" };
		foreach (var game in _gamesNotInInterestListOnCurrentPage)
		{
			options.Add("ID: " + game.ID + " Name: " + game.Name);
		}
		return options;
	}
	#endregion

	#region Methods that get the correct gameId from the different menus to add/delete games from interest list
	public void GetSelectedGameFromInterestList(int gameId)
	{
		var currentGameId = _gamesInInterestListOnCurrentPage[gameId].ID;
		var game = _gameRepo.GetGameInfo(currentGameId);
		string gameDetails = _gameInfoView.ShowGameDetails(game);
		GetSelectedGameFromAllMenus(currentGameId, gameDetails);
	}

	public void GetSelectedGameFromAddToInterestMenu(int gameId)
	{
		int currentGameId = _gamesNotInInterestListOnCurrentPage[gameId].ID;
		var game = _gameRepo.GetGameInfo(currentGameId);
		string gameDetails = _gameInfoView.ShowGameDetails(game);
		GetSelectedGameFromAllMenus(currentGameId, gameDetails);
	}

	public void GetSelectedGameFromRecommendMenu(int gameId)
	{
		var game = _gameRepo.GetGameInfo(gameId);
		string gameDetails = _gameInfoView.ShowGameDetails(game);
		GetSelectedGameFromAllMenus(gameId, gameDetails);
	}
	#endregion

	#region Add/remove/list games on interest list
	public void GetSelectedGameFromAllMenus(int currentGameId, string gameDetails)
	{
		CurrentIndex = 1;
		if (CheckInterestState(currentGameId) == false)
		{
			List<string> options = new List<string> { "Add to interest list", "Return to previous menu" };
			
			var selectedIndex = _menuLogic.CallMenu(gameDetails, options, CurrentIndex, _notification);
			CurrentIndex = selectedIndex;

			switch (selectedIndex)
			{
				case 0:
					_interestRepo.AddGameToInterest(currentGameId);
					break;
				case 1:
					break;
			}
		}
		else
		{
			List<string> options = new List<string> { "Remove from interest list", "Return to previous menu" };
			var selectedIndex = _menuLogic.CallMenu(gameDetails, options, CurrentIndex, _notification);
			CurrentIndex = selectedIndex;

			switch (selectedIndex)
			{
				case 0:
					_interestRepo.RemoveGameFromInterest(currentGameId);
					break;
				case 1:
					break;
			}
		}
	}

	public bool CheckInterestState(int gameId)
	{
		var list = _interestRepo.GetGamesOnInterestList(_currentPage);
		foreach (var game in list)
		{
			if (gameId == game.ID)
			{
				return true;
			}
		}
		return false;
	}

	public List<string> GetGamesOnInterestListWithOptions()
	{
		_numberOfGamesInList = (_interestRepo.CountGamesInInterestList());
		List<string> options = new List<string> { "Back to main menu", "Next page", "Previous page", "Add games to interest list", "Look for recommendations", "---- Games on list: " + _numberOfGamesInList + " -----" };
		foreach (var game in _gamesInInterestListOnCurrentPage)
		{
			options.Add("ID: " + game.ID + " Name: " + game.Name);
		}
		return options;
	}

	public void ListInterested()
	{
		var gamesInterested = _interestRepo.GetGamesOnInterestList(GetCurrentPage());
		_gamesInInterestListOnCurrentPage.Clear();
		GamesAddedToList(gamesInterested);
	}

	private void GamesAddedToList(List<GameObject> games)
	{
		foreach (var game in games)
		{
			GameObject gameObject = new GameObject(game.ID, game.Name);
			_gamesInInterestListOnCurrentPage.Add(gameObject);
		}
	}

	public void CheckCurrentPageAndDisplayInterestList(int i)
	{
		if (i == 1 && GetCurrentPage() < _numberOfGamesInList)
		{
			int j = GetCurrentPage();
			SetCurrentPage(j += 10);
		}
		else if (i == 2 && GetCurrentPage() != _firstPage)
		{
			int j = GetCurrentPage();
			SetCurrentPage(j -= 10);
		}
		ListInterested();
	}
	#endregion
}