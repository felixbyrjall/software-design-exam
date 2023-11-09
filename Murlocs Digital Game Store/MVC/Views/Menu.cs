using DigitalGameStore.Model;
using DigitalGameStore.Controller;
using DigitalGameStore.MVC.Views;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class Menu {    

    private readonly ListGamesController? _listGamesController;

    public void MainMenu()
    {
        var gameRepo = new Repo.GameRepo();
        var browseView = new BrowseView(_listGamesController);
        var listGamesController = new ListGamesController(gameRepo, browseView);

        string additionalText = "(Use the arrows to select an option)";
        string[] menuOptions = { "Browse Games", "Interest list", "Recommendations", "Exit" };
        MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions);

        int selectedIndex = mainMenu.Start();

        switch (selectedIndex)
        {
            case 0:
                listGamesController.StartPage();
                browseView.BrowseMenu();
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

    public void ReturnToMainMenu()
    {
        MainMenu();
    }
}
