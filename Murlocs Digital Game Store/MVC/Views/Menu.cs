using DigitalGameStore.Model;
using DigitalGameStore.Controller;
using DigitalGameStore.Views;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class Menu {

	BrowseView view = new();
	MenuLogic menuTools = new();

	private List<String> menuOptions = new List<string>{ "Browse Games", "Interest List", "Recommendations", "Exit" };
	private string _prompt = "(Use the arrows to select an option)";
	public static int currentIndex = 0;

	public void MainMenu()
    {
		var selectedIndex = menuTools.CallMenu(_prompt, menuOptions, currentIndex);
		currentIndex = selectedIndex;

		switch (selectedIndex)
        {
            case 0:
				view.DisplayGameList(selectedIndex);
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
		var selectedIndex = menuTools.CallMenu(_prompt, BrowseView._allGames, currentIndex);
		currentIndex = selectedIndex;
		BrowseView._allGames.Clear();

		switch (selectedIndex)
		{
			case 0:
				ReturnToMainMenu();
				break;
			case 1:
			case 2:
				view.DisplayGameList(selectedIndex);
				BrowseMenu();
				break;
		}
	}

	public void ReturnToMainMenu()
    {
        MainMenu();
    }
}
