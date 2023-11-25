using NextGaming.Interfaces;
using NextGaming.Model;
using NextGaming.Repo;
using NextGaming.Tools;
using NextGaming.Views;

namespace NextGaming.Controller;

public class InterestController
{
    private int _currentPage = 10;
    private int _lastPage;
    private const int _firstPage = 10;
    private readonly IInterestRepo _interestRepo;
    private readonly InterestView _interestView;
    private readonly GameRepo _gameRepo;
	private readonly BrowseController _browseController;
	private readonly MenuLogic _menuLogic;
	private readonly GameInfoView _gameDisplay;

	public static int currentIndex = 0;

	public InterestController(IInterestRepo interestRepo, InterestView interestView, GameRepo gameRepo, BrowseController browseController, MenuLogic menuLogic, GameInfoView gameDisplay)
    {
        _interestRepo = interestRepo;
        _interestView = interestView;
        _gameRepo = gameRepo;
		_browseController = browseController;
		_menuLogic = menuLogic;
		_gameDisplay = gameDisplay;
    }

    private List<GameObject> _gamesNotAdded = new();

    private void GamesNotAddedToList(List<GameObject> games)
    {
        foreach (var game in games)
        {
            GameObject gameObject = new GameObject(game.ID, game.Name);
            _gamesNotAdded.Add(gameObject);
        }
    }

    public void ListNotInterested()
    {
        var gamesInterested = _interestRepo.GetNotInterestedGames(GetCurrentPage());
        _gamesNotAdded.Clear();
        GamesNotAddedToList(gamesInterested);
    }

    public void Check(int i)
    {
        if (i == 1 && GetCurrentPage() < _lastPage)
        {
            int j = GetCurrentPage();
            SetCurrentPage(j += 10);
        }
        else if (i == 2 && GetCurrentPage() != _firstPage)
        {
            int j = GetCurrentPage();
            SetCurrentPage(j -= 10);
        }
        ListNotInterested();
    }

    public List<string> GetGamesOnPageWithOptions()
    {
        _lastPage = _interestRepo.CountGamesNotInInterestList();
        List<string> options = new List<string> { "Back to main menu", "Next page", "Previous page", "---- Games remaining: " + _lastPage + " -----" };
        foreach (var game in _gamesNotAdded)
        {
            options.Add("ID: " + game.ID + " Name: " + game.Name);
        }
        return options;
    }

    public int GetCurrentPage()
    {
        return _currentPage;
    }

    public void SetCurrentPage(int currentPage)
    {
        _currentPage = currentPage;
    }

    // Forebygger skade hvis man trykker enter på et tomt felt. (Gjelder kun foreløpig display)
    public void AddInterest(int gameId)
    {
        if (_gamesNotAdded.Count() == 0)
        {
            int j = GetCurrentPage();
            SetCurrentPage(j -= 10);
        }
        else
        {
            _interestRepo.AddGameToInterest(_gamesNotAdded[gameId].ID);
        }
    }

    public bool CheckInterestState(int gameID)
    {
        var list = _interestRepo.GetGamesOnInterestList(_currentPage);
        foreach (var game in list)
        {
            if (gameID == game.ID)
            {
                return true;
            }
        }
        return false;
    }

    public void GetSelectedGame(int gameId)
	{
		int currentGameId = _gamesNotAdded[gameId].ID;
		var game = _gameRepo.GetGameInfo(currentGameId);

		if (CheckInterestState(gameId) == false)
		{
			List<string> options = new List<string> { "Add to interest list", "Return to previous menu" };
			var selectedIndex = _menuLogic.CallMenu(_gameDisplay.ShowGameDetails2(game), options, currentIndex);
			currentIndex = selectedIndex;

			switch (selectedIndex)
			{
				case 0:
					_interestRepo.AddGameToInterest(gameId);
					break;
				case 1:
					break;

			}
		}
		else
		{
			List<string> options = new List<string> { "Remove from interest list", "Return to previous menu" };
			var selectedIndex = _menuLogic.CallMenu(_gameDisplay.ShowGameDetails2(game), options, currentIndex);
			currentIndex = selectedIndex;

			switch (selectedIndex)
			{
				case 0:
					_interestRepo.RemoveGameFromInterest(gameId);
					break;
				case 1:
					break;

			}
		}

	}


	//
	//
	//
	//

	private List<GameObject> _gamesInInterestList = new();

	public List<string> GetGamesOnInterestListWithOptions()
	{
		_lastPage = _interestRepo.CountGamesInInterestList();

		int numberOfGamesInList = 2;
		List<string> o = new List<string> { "Back to main menu", "Next page", "Previous page", "Add games to interest list", "Look for recommendations","---- Games on list: " + numberOfGamesInList + " -----" };
		foreach (var game in _gamesInInterestList)
		{
			o.Add("ID: " + game.ID + " Name: " + game.Name);
		}
		return o;
	}

	public void ListInterested()
	{
		var gamesInterested = _interestRepo.GetGamesOnInterestList(GetCurrentPage());
		_gamesInInterestList.Clear();
		GamesAddedToList(gamesInterested);
	}

	private void GamesAddedToList(List<GameObject> games)
	{
		foreach (var game in games)
		{
			GameObject gameObject = new GameObject(game.ID, game.Name);
			_gamesInInterestList.Add(gameObject);
		}
	}

	public void Check2(int i)
	{
		if (i == 1 && GetCurrentPage() < _lastPage)
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

	public void RemoveInterest(int gameId)
	{
		_interestRepo.RemoveGameFromInterest(gameId);
	}

}