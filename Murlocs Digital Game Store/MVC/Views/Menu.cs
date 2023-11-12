using DigitalGameStore.Model;
using DigitalGameStore.Controller;
using DigitalGameStore.Views;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class Menu {

    private readonly MenuLogic _menuTools;
	private readonly BrowseController _browseController;
	private readonly InterestController _interestController;
	private readonly RecommendController _recommendController;

    public Menu(MenuLogic menuTools, BrowseController browseController, InterestController interestController, RecommendController recommendController)
    {
        _menuTools = menuTools;
        _browseController = browseController;
		_interestController = interestController;
		_recommendController = recommendController;
    }

    private List<String> menuOptions = new List<string>{ "Browse Games", "Interest List", "Recommendations", "Exit" };
	private string _prompt = "(Use the arrows to select an option)";
	public static int currentIndex = 0;

	public void MainMenu()
    {
		var selectedIndex = _menuTools.CallMenu(_prompt, menuOptions, currentIndex);
		currentIndex = selectedIndex;

		switch (selectedIndex)
        {
            case 0: // Browse games
				Func.Clear();
				_browseController.Check(selectedIndex);
                BrowseMenu();
				break;
			case 1: // See list of interested games
                _interestController.GetGamesOnInterestListWithOptions();
                _interestController.Check2(selectedIndex);
                ShowInterestList();
				break;
            case 2:
	            _recommendController.GetRecommendedGameWithOptions();
	            RecommendMenu();
				break;
			case 3: // Exit the application
                Environment.Exit(0);
                break;
        }
    }

	public void BrowseMenu()
	{
        List<string> gamesWithOptions = _browseController.GetGamesOnPageWithOptions();

        var selectedIndex = _menuTools.CallMenu(_prompt, gamesWithOptions, currentIndex);
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
				Console.ReadLine();
				break;
		}
	}

    public void ShowInterestList()
    {
        List<string> interestListWithOptions = _interestController.GetGamesOnInterestListWithOptions();

		var selectedIndex = _menuTools.CallMenu(_prompt, interestListWithOptions, currentIndex);
        currentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0:
				ReturnToMainMenu(); // Return to main menu
				break;
            case 1:
                _interestController.GetGamesOnPageWithOptions();
				_interestController.Check(selectedIndex - 1);
				InterestMenu();
				break;
            case 2:
                ShowInterestList(); // placeholder for recommendations
				break;
            case 3:
                ShowInterestList(); // Line
                break;
			default:
				_interestController.RemoveInterest((selectedIndex - 4));
				_interestController.GetSelectedGame((selectedIndex - 4));
				_interestController.Check(selectedIndex);
				Console.ReadLine();
				InterestMenu();
				break;
		}

	}

    public void InterestMenu()
    {
        List<string> gamesWithOptions = _interestController.GetGamesOnPageWithOptions();

        var selectedIndex = _menuTools.CallMenu(_prompt, gamesWithOptions, currentIndex);
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
                _interestController.AddInterest((selectedIndex - 4));
                _interestController.GetSelectedGame((selectedIndex - 4));
                _interestController.Check(selectedIndex);
                Console.ReadLine();
                InterestMenu();
                break;
        }
    }
    public void RecommendMenu() {
	    List<string> gamesWithOptions = _recommendController.GetRecommendedGameWithOptions();
	    var selectedIndex = _menuTools.CallMenu(_prompt, gamesWithOptions, currentIndex);	    
	    switch (selectedIndex)
	    {
		    case 0:
			    ReturnToMainMenu();
			    break;
		    default:   
			    _recommendController.GetSelectedGame(selectedIndex);
			    Console.ReadLine();
			    RecommendMenu();
			    break;
		    
	    }

    }

    public void ReturnToMainMenu()
    {
        MainMenu();
    }
}
