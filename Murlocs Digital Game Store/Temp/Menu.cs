using System;
using System.Data;
using System.Runtime.CompilerServices;
using DB;
using DigitalGameStore.DB;
using DigitalGameStore.InterestList;
using DigitalGameStore.RecommendGames;
using DigitalGameStore.UI;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.Login;

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
				InterestList(); // Browse your games (edit/delete) --> add more games --> BrowseMenu()
                break;
			case 2:
				RecommendGames(); // Browse recommended games --> add to interestlist (Refreshes recommendedgameslist)
				break;
			case 3:
                Environment.Exit(0);
                break;
        }
    }

    public void BrowseMenu()
    {

    }

    public void InterestList()
    {

        AddGame addGame = new AddGame();
        DeleteGame deleteGame = new DeleteGame();
        DisplayList displayList = new DisplayList();

        string additionalText = "(Use the arrows to select an option)";
        string[] menuOptions = { "Display List", "Add Interest", "Delete Interest", "Display All Games", "Exit" };
        MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions);

        int selectedIndex = mainMenu.Start();

        switch (selectedIndex)
        {

            case 0:
				Func.WriteOutput("Here is your Interest List: ");
                displayList.DisplayInterest();
                break;
            case 1:
                Func.WriteOutput("What game would you like to add from your interest list?: ");
				int addInput = int.Parse(Func.ReadInput());
                addGame.Add(addInput);
                InterestList();
                break;
            case 2:
				Func.WriteOutput("What game would you like to delete from your interest list?: ");
                int deleteInput = int.Parse(Func.ReadInput());
                deleteGame.Delete(deleteInput);
                InterestList();
                break;
            case 3:
                displayList.DisplayAllGames();
                break;
            case 4:
                Environment.Exit(0);
                break;

        }
    }

    public async Task RecommendGamesAsync()
    {
        using (var context = new Context()) // Use the correct context class name
        {
            var interestAnalyzer = new InterestAnalyzer(context);
            var gameRecommender = new GameRecommender(context);

            var recommendedGames = await gameRecommender.RecommendGames(interestAnalyzer);

            foreach (var game in recommendedGames)
            {
                Console.WriteLine($"{game.Name} - Score: {game.Score}");
            }

			Func.WriteOutput("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }

    // Make sure to call this method asynchronously from the menu option.
    public void RecommendGames()
    {
        RecommendGamesAsync().GetAwaiter().GetResult(); // This will synchronously wait for the async operation.
    }
}
