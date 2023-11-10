using DigitalGameStore.Model;
using DigitalGameStore.Controller;
using DigitalGameStore.Views;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class Menu {
    private readonly MenuLogic _menuTools;
	private readonly BrowseController _browseController;

    public Menu( MenuLogic menuTools, BrowseController browseController)
    {
        _menuTools = menuTools;
        _browseController = browseController;
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
	            _browseController.GetAllGamesWithOptions();
	            _browseController.Check(selectedIndex);
                BrowseMenu();
				break;
			case 1:
				_browseController.GetAllGamesWithOptions();
				_browseController.NotInterestedGames(selectedIndex - 1);
				InterestMenu();
                break;
			case 2:
				_browseController.AddInterest(5);
				ReturnToMainMenu();
				break;
			case 3:
                Environment.Exit(0);
                break;
        }
    }

	public void BrowseMenu()
	{
        List<string> gamesWithOptions = _browseController.GetAllGamesWithOptions();

        var selectedIndex = _menuTools.CallMenu(_prompt, gamesWithOptions, currentIndex);
		currentIndex = selectedIndex;

		switch (selectedIndex)
		{
			case 0:
				ReturnToMainMenu();
				break;
			case 1:
			case 2:
				_browseController.Check(selectedIndex);
				BrowseMenu();
				break;
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
				_browseController.GetSelectedGame((selectedIndex - 3));
				Console.ReadLine();
				BrowseMenu();
				break;
		}
	}
	
	public void InterestMenu()
	{
		List<string> gamesWithOptions = _browseController.GetAllGamesWithOptions();

		var selectedIndex = _menuTools.CallMenu(_prompt, gamesWithOptions, currentIndex);
		currentIndex = selectedIndex;

		switch (selectedIndex)
		{
			case 0:
				ReturnToMainMenu();
				break;
			case 1:
			case 2:
				_browseController.NotInterestedGames(selectedIndex);
				InterestMenu();
				break;
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
				_browseController.AddInterest((selectedIndex - 3));
				_browseController.GetSelectedGame((selectedIndex - 3));
				_browseController.NotInterestedGames(selectedIndex);
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
