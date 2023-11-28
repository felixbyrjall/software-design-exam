using DigitalGameStore.MVC.Controller;
using NextGaming.Model;
using NextGaming.Controller;
using NextGaming.Views;
using NextGaming.Tools;
using NextGaming.Interfaces;
using NextGaming.Repo;

namespace NextGaming.Views;

public class MenuController
{
	#region Fields and dependencies
	private readonly MenuLogic _menuLogic;
	private readonly BrowseController _browseController;
	private readonly InterestController _interestController;
	private readonly RecommendController _recommendController;
	private readonly IInterestRepo _interestRepo;
	private readonly NotificationController _notificationController;

	private string _prompt = "(Use the arrows to select an option)";
	private string _notification = "";
	public int CurrentIndex = 0;

	public MenuController(MenuLogic menuTools, BrowseController browseController, InterestController interestController, RecommendController recommendController, IInterestRepo interestRepo, NotificationController notificationController)
    {
        _menuLogic = menuTools;
        _browseController = browseController;
		_interestController = interestController;
		_recommendController = recommendController;
		_interestRepo = interestRepo;
		_notificationController = notificationController;
    }
	#endregion

	#region Menus
	public void MainMenu()
	{
		CurrentIndex = 0;

		_notificationController.Navigated += OnNavigate;
	    
		List<String> menuOptions = new List<string> { "Browse Games", "Interest List", "Recommendations", "Reset interest list", "Exit" };
		var selectedIndex = _menuLogic.CallMenu(_prompt, menuOptions, CurrentIndex, _notification);
		CurrentIndex = selectedIndex;

		switch (selectedIndex)
        {
            case 0: // Browse games
	            _browseController.CheckLoading();
				_browseController.ListGames();
				_notificationController.OnNavigate();
				BrowseMenu();
				break;
			case 1: // See list of games added to interest list
				_browseController.CheckLoading();
                _interestController.GetGamesOnInterestListWithOptions();
                _interestController.ListInterested();
				CurrentIndex = 0;
                _notificationController.OnNavigate();
				ShowInterestList();
				break;
            case 2: // 
	            _recommendController.GetRecommendedGameWithOptions();
                _recommendController.ListRecommendedGames();
				CurrentIndex = 0;
	            _notificationController.OnNavigate();
				RecommendMenu();
				break;
            case 3:
                ResetMenu();
                MainMenu();
                break;
			case 4: // Exit the application
				Environment.Exit(0);
				break;
		}
    }

	public void BrowseMenu()
	{
        List<string> gamesWithOptions = _browseController.GetGamesOnPageWithOptions();
        
        _notificationController.Changed += OnChange;
        _notificationController.Navigated += OnNavigate;

        var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, CurrentIndex, _notification);
		CurrentIndex = selectedIndex;

		switch (selectedIndex)
		{
			case 0: // Return to Main Menu
				ReturnToMainMenu();
				break;
			case 1: // Next Page
            case 2: // Previous Page
				Console.Clear();
				_browseController.CheckCurrentPage(selectedIndex);
				BrowseMenu();
				break;
            case 3: // Divider between menu options and interactive objects.
                BrowseMenu();
                break;
            default: // Displayed objects
				_browseController.GetSelectedGameFromBrowseMenu((selectedIndex - 3) + _browseController.GetCurrentPage() - 10);
				BrowseMenu();
				break;
		}
	}

    // Lists all games added to interest list
    public void ShowInterestList()
    {
        List<string> interestListWithOptions = _interestController.GetGamesOnInterestListWithOptions();
        
        _notificationController.Changed += OnChange;
        _notificationController.Navigated += OnNavigate;

		if (CurrentIndex+1 > interestListWithOptions.Count())
		{
			CurrentIndex--;
		}

		var selectedIndex = _menuLogic.CallMenu(_prompt, interestListWithOptions, CurrentIndex, _notification);
        CurrentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0:
	            _notificationController.OnNavigate();
				ReturnToMainMenu(); // Return to main menu
				break;
            case 1:
            case 2:
				Console.Clear(); // NEXT AND PREVIOUS PAGE
				_interestController.CheckCurrentPageAndDisplayInterestList(selectedIndex);
				ShowInterestList();
				break;
            case 3:
                _notificationController.OnNavigate();
                _interestController.GetGamesOnPageWithOptions(); //ADD GAMES TO INTEREST LIST
				_interestController.ListNotInterestedOnCurrentPage();
                CurrentIndex = 0;
				InterestMenu();
                break;
			case 4:
                _notificationController.OnNavigate();
                _recommendController.GetRecommendedGameWithOptions(); // LOOK FOR RECOMMENDATIONS
				_recommendController.ListRecommendedGames();
				CurrentIndex = 0;
				RecommendMenu();
				break;
			case 5:
                ShowInterestList(); // Line
                break;
			default:
				_interestController.GetSelectedGameFromInterestList((selectedIndex - 6));
				_interestController.ListInterested();
				ShowInterestList();
				break;
		}
	}

    // Lists all games that are NOT on the users interest list
    public void InterestMenu()
    {
        List<string> gamesWithOptions = _interestController.GetGamesOnPageWithOptions();
        
        _notificationController.Changed += OnChange;
        _notificationController.Navigated += OnNavigate;

		if (CurrentIndex + 1 > gamesWithOptions.Count())
		{
			CurrentIndex--;
		}

		var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, CurrentIndex, _notification);
        CurrentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0: // Return to main menu
				_interestController.SetCurrentPage(10);
				_interestController.ListInterested();
                _notificationController.OnNavigate();
                ShowInterestList();
                break;
            case 1: // Next Page
            case 2: // Previous Page
                _interestController.CheckCurrentPageAndDisplayGamesNotOnInterestList(selectedIndex);
                InterestMenu();
                break;
            case 3: // Divider between menu options and interactive objects.
                InterestMenu();
                break;
            default: // Displayed objects
				_interestController.GetSelectedGameFromAddToInterestMenu((selectedIndex - 4));
				_interestController.ListNotInterestedOnCurrentPage();
				InterestMenu();
                break;
        }
    }

	public void RecommendMenu()
	{
	    List<string> gamesWithOptions = _recommendController.GetRecommendedGameWithOptions();
	    
	    _notificationController.Changed += OnChange;
	    _notificationController.Navigated += OnNavigate;

		if (CurrentIndex + 1 > gamesWithOptions.Count())
		{
			CurrentIndex--;
		}

		var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, CurrentIndex, _notification);
		CurrentIndex = selectedIndex;
		switch (selectedIndex)
	    {
            case 0: // Return to main menu
                ReturnToMainMenu();
                break;
            case 1: // Divider between menu options and interactive objects.
				RecommendMenu();
				break;
            default:
                _recommendController.GetSelectedGameFromRecommendMenu(selectedIndex-2);
				_recommendController.ListRecommendedGames();
				RecommendMenu();
			    break;
	    }
    }

	public void ResetMenu()
	{
		List<string> options = new List<string> { "Clear interest list", "Return to main menu" };
		var selectedIndex = _menuLogic.CallMenu(_prompt, options, 1, _notification);
		CurrentIndex = selectedIndex;
		switch (selectedIndex)
		{
			case 0:
				ClearInterestList();
				break;
			case 1:
				ReturnToMainMenu();
				break;
		}
	}
	#endregion

	#region Event handler methods
	public void OnNavigate(object? source, EventArgs e)
	{
		_notification = "";
	}

    public void OnChange(object? source, ChangeArgs e)
    {
        if (e.method == "add")
        {
            _notification = $"GameID: {e.gameId} has been added to the interest list!";
        }

        else if (e.method == "remove")
        {
            _notification = $"GameID: {e.gameId} has been removed from the interest list!";
        }
    }
    
    public void OnClear(object? source, EventArgs e)
    {
	    _notification = "Your interest list has been cleared!";
    }
	#endregion

	#region Misc methods
	public void ReturnToMainMenu()
    {
	    _notificationController.OnNavigate();
		_browseController.SetCurrentPage(10);
        _interestController.SetCurrentPage(10);
		MainMenu();
    }

	// Mainly for debugging logic error
	public void ClearInterestList()
	{
		_notificationController.Cleared += OnClear;
		
		Console.Clear();
		for (int i = 1; i <= 100; i++)
		{
			_interestRepo.RemoveGameFromInterest(i);
		}
		_notificationController.OnNavigate();
		_notificationController.OnClear();
	}
	#endregion
}