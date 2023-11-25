using NextGaming.Model;
using NextGaming.Controller;
using NextGaming.Views;
using NextGaming.Tools;
using NextGaming.Interfaces;
using NextGaming.Repo;

namespace NextGaming.Views;

public class MenuController {

    private readonly MenuLogic _menuLogic;
	private readonly BrowseController _browseController;
	private readonly InterestController _interestController;
	private readonly RecommendController _recommendController;
	private readonly IInterestRepo _interestRepo;

	private string _prompt = "(Use the arrows to select an option)";
	public static int currentIndex = 0;

	public MenuController(MenuLogic menuTools, BrowseController browseController, InterestController interestController, RecommendController recommendController, IInterestRepo interestRepo)
    {
        _menuLogic = menuTools;
        _browseController = browseController;
		_interestController = interestController;
		_recommendController = recommendController;
		_interestRepo = interestRepo;
	}

    // Mainly for debugging logic error
    public void ClearInterestList()
    {
        Console.Clear();
        for (int i = 0; i < 100; i++)
        {
			_interestRepo.RemoveGameFromInterest(i);
		}
        Console.WriteLine("Interest list cleared");
        Console.WriteLine("Press any KEY to go back to Main menu");
        Console.ReadLine();
    }

	public void MainMenu()
    {
		List<String> menuOptions = new List<string> { "Browse Games", "Interest List", "Recommendations", "Exit", "Reset interest list" };
		var selectedIndex = _menuLogic.CallMenu(_prompt, menuOptions, currentIndex);
		currentIndex = selectedIndex;

		switch (selectedIndex)
        {
            case 0: // Browse games
				Func.Clear();
				_browseController.ListGames();
				BrowseMenu();
				break;
			case 1: // See list of games added to interest list
                _interestController.GetGamesOnInterestListWithOptions();
                _interestController.ListInterested();
				ShowInterestList();
				break;
            case 2: // 
	            _recommendController.GetRecommendedGameWithOptions();
                _recommendController.ListRecommendedGames();
                RecommendMenu();
				break;
			case 3: // Exit the application
                Environment.Exit(0);
                break;
            case 4:
                ClearInterestList();
                MainMenu();
                break;
        }
    }

	public void BrowseMenu()
	{
        List<string> gamesWithOptions = _browseController.GetGamesOnPageWithOptions();

        var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, currentIndex);
		currentIndex = selectedIndex;

		switch (selectedIndex)
		{
			case 0: // Return to Main Menu
				ReturnToMainMenu();
				break;
			case 1: // Next Page
            case 2: // Previous Page
				Func.Clear();
				_browseController.Check(selectedIndex);
				BrowseMenu();
				break;
            case 3: // Divider between menu options and interactive objects.
                BrowseMenu();
                break;
            default: // Displayed objects
				_browseController.GetSelectedGame((selectedIndex - 3) + _browseController.GetCurrentPage() - 10);
				BrowseMenu();
				break;
		}
	}

    // Lists all games added to interest list
    public void ShowInterestList()
    {
        List<string> interestListWithOptions = _interestController.GetGamesOnInterestListWithOptions();

		var selectedIndex = _menuLogic.CallMenu(_prompt, interestListWithOptions, currentIndex);
        currentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0:
				ReturnToMainMenu(); // Return to main menu
				break;
            case 1:
            case 2:
				Func.Clear(); // NEXT AND PREVIOUS PAGE
				_interestController.Check2(selectedIndex);
				ShowInterestList();
				break;
            case 3:
				_interestController.GetGamesOnPageWithOptions(); //ADD GAMES TO INTEREST LIST
				_interestController.Check(selectedIndex - 1);
				InterestMenu();
                break;
			case 4:
				_recommendController.GetRecommendedGameWithOptions(); // LOOK FOR RECOMMENDATIONS
				_recommendController.ListRecommendedGames();
				RecommendMenu();
				break;
			case 5:
                ShowInterestList(); // Line
                break;
			default:
				_interestController.GetSelectedGame((selectedIndex - 6));
				_interestController.Check(selectedIndex);
                ShowInterestList();
				break;
		}

	}

    // Lists all games that are NOT on the users interest list
    public void InterestMenu()
    {
        List<string> gamesWithOptions = _interestController.GetGamesOnPageWithOptions();

        var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, currentIndex);
        currentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0: // Return to main menu
                ReturnToMainMenu();
                break;
            case 1: // Next Page
            case 2: // Previous Page
                _interestController.Check(selectedIndex);
                InterestMenu();
                break;
            case 3: // Divider between menu options and interactive objects.
                InterestMenu();
                break;
            default: // Displayed objects
				_interestController.GetSelectedGame((selectedIndex - 4));
                _interestController.Check(selectedIndex);
                InterestMenu();
                break;
        }
    }

    public void RecommendMenu() {
	    List<string> gamesWithOptions = _recommendController.GetRecommendedGameWithOptions();
        var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, currentIndex);
		currentIndex = selectedIndex;
		switch (selectedIndex)
	    {
            case 0: // Return to main menu
                ReturnToMainMenu();
                break;
            case 1: // Next Page
            case 2: // Previous Page
                RecommendMenu();
                break;
            case 3: // Divider between menu options and interactive objects.
                RecommendMenu();
                break;
            default:
                _recommendController.GetSelectedGame(selectedIndex-4);
			    RecommendMenu();
			    break;
	    }

    }

    public void ReturnToMainMenu()
    {
		_browseController.SetCurrentPage(10);
        _interestController.SetCurrentPage(10);
		MainMenu();
    }
}
