using NextGaming.Interfaces;
using NextGaming.Views;
using NextGaming.Model;
using NextGaming.Tools;

namespace NextGaming.Controller;

public class BrowseController
{
    #region Fields and dependencies
	private const int _firstPage = 10;
	private int _currentPage = 10;
	private int _lastPage;
    private bool _gamesLoaded = false;

	private List<String> _gamesOnPage = new();

	private readonly IGameRepo _gameRepo;
    private readonly BrowseView _browseView;
    private readonly GameInfoView _gameInfoView;
    private readonly InterestController _interestController;

    public BrowseController(IGameRepo gameRepo, BrowseView browseView, GameInfoView gameInfoView, InterestController interestController)
    {
        _gameRepo = gameRepo;
        _browseView = browseView;
        _gameInfoView = gameInfoView;
        _interestController = interestController;
    }
	#endregion

	#region Getter and setter for current page
	public int GetCurrentPage()
    {
        return _currentPage;
    }

    public void SetCurrentPage(int currentPage)
    {
        _currentPage = currentPage;
    }
	#endregion

	#region Methods for checking page, listing and adding games
	public void CheckCurrentPage(int i)
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
        var games = _gameRepo.GetGamesOnPage((GetCurrentPage() - 9), GetCurrentPage());
        _gamesOnPage.Clear();
        AddGamesToMenu(games); // Kaller på metoden AddGames for å legge til spill i _allGames feltet i view.
    }

	private void AddGamesToMenu(IEnumerable<Game> games)
	{
		foreach (Game game in games)
		{
			_gamesOnPage.Add("ID: " + game.ID + " Name: " + game.Name);
		}
	}

	public List<string> GetGamesOnPageWithOptions()
    {
        _lastPage = _gameRepo.CountAllGames();
        List<string> options = new List<string> { "Back to main menu", "Next page", "Previous page", "---------" };
        options.AddRange(_gamesOnPage);
        return options;
    }

    public void GetSelectedGameFromBrowseMenu(int gameId)
    {
		var game = _gameRepo.GetGameInfo(gameId);
		string gameDetails = _gameInfoView.ShowGameDetails(game);
        _interestController.GetSelectedGameFromAllMenus(gameId, gameDetails);
	}
	#endregion

	#region Loading method
	public int LoadingTime()
    {
        int totalLoadingTime = _gameRepo.CountAllGames() * 5;
        return totalLoadingTime;
    }
	
	public void CheckLoading()
	{
		var totalTime = LoadingTime();
		if (_gamesLoaded == false)
		{
			_browseView.LoadingScreen(totalTime);
			_gamesLoaded = true;
		}
	}
	#endregion
}
