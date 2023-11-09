using DigitalGameStore.Model;
using DigitalGameStore.Controller;
using DigitalGameStore.Views;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class Menu {

    private readonly BrowseView _view;
    private readonly MenuLogic _menuTools;
	private readonly BrowseController _browseController;

    public Menu(BrowseView browseView, MenuLogic menuTools, BrowseController browseController)
    {
        _view = browseView;
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
				_view.DisplayGameList(selectedIndex);
                BrowseMenu();
				break;
			case 1:
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
				_view.DisplayGameList(selectedIndex);
				BrowseMenu();
				break;
		}
	}

	public void ReturnToMainMenu()
    {
        MainMenu();
    }
}
