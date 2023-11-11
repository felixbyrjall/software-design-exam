using DigitalGameStore.Model;
using DigitalGameStore.Controller;
using DigitalGameStore.Views;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class Menu {

    private readonly MenuLogic _menuTools;
	private readonly BrowseController _browseController;
	private readonly InterestController _interestController;

    public Menu(MenuLogic menuTools, BrowseController browseController, InterestController interestController)
    {
        _menuTools = menuTools;
        _browseController = browseController;
		_interestController = interestController;
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
            case 0:
				Func.Clear();
				_browseController.Check(selectedIndex);
                BrowseMenu();
				break;
			case 1:
                _interestController.GetGamesOnPageWithOptions();
                _interestController.Check(selectedIndex - 1);
                InterestMenu();
				break;
            case 2:
				break;
			case 3:
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
            case 2: // Previopus Page
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

    public void InterestMenu()
    {
        List<string> gamesWithOptions = _interestController.GetGamesOnPageWithOptions();

        var selectedIndex = _menuTools.CallMenu(_prompt, gamesWithOptions, currentIndex);
        currentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0:
                ReturnToMainMenu();
                break;
            case 1:
            case 2:
                _interestController.Check(selectedIndex);
                InterestMenu();
                break;
            case 3: // Divider between menu options and interactive objects.
                InterestMenu();
                break;
            default:
                _interestController.AddInterest((selectedIndex - 4));
                _interestController.GetSelectedGame((selectedIndex - 4));
                _interestController.Check(selectedIndex);
                Console.ReadLine();
                InterestMenu();
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        MainMenu();
    }
}
