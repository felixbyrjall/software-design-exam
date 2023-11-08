using DigitalGameStore.DB;
using DigitalGameStore.Controller;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class Menu {

    private Context? _context;

    public void MainMenu()
    {

        string additionalText = "(Use the arrows to select an option)";
        string[] menuOptions = { "Browse Games", "Interest list", "Recommendations", "Exit" };
        MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions);

        int selectedIndex = mainMenu.Start();

        switch (selectedIndex)
        {
            case 0:
				BrowseMenu(); // On enter --> Add game to interest list or read more about game or back
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

        string additionalText = "(Use the arrows to select an option)";
        string[] menuOptions = { "Browse Games", "Interest list", "Recommendations", "Exit" };
        MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions);

        // Ny stuff
        var gameModel = new Model.GameModel();
        var browseView = new Views.BrowseView();
        var listGamesController = new ListGamesController(gameModel, browseView);
        listGamesController.ListGames();

        int selectedIndex = mainMenu.Start();

        switch (selectedIndex)
        {
            case 0:
                break;
            case 1:
                break;
        }

    }

}
