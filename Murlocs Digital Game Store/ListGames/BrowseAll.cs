using DB;
using DigitalGameStore.DB;
using DigitalGameStore.Login;
using DigitalGameStore.UI;

namespace DigitalGameStore.Browse;

public class BrowseAll
{
    private int _index = 10;

    public void AllGames()
    {

        List<String> gameSelections = new List<String>();
        List<int> gameIDs = new List<int>();
        gameSelections.Add("Previous Menu");
        gameSelections.Add("Next 10 Games");
        gameSelections.Add("Previous 10 Games");
        using Context database = new();

        IList<Game> allGames = database.Game.ToList();
        foreach (var game in allGames)
        {

            if (game.ID >= (_index - 10) && game.ID <= _index)
            {

                gameSelections.Add(game.Name);
                gameIDs.Add(game.ID);
            }
        }

        string prompt = "(Use the arrows to select an option)";
        string[] options = gameSelections.ToArray();
        MenuLogic mainMenu = new MenuLogic(prompt, options);
        GameInfo gameInfo = new GameInfo();
        Menu menu = new Menu();

        int selectedIndex = mainMenu.Start();

        switch (selectedIndex)
        {

            case 0:
                menu.BrowseMenu();
                break;
            case 1:
                if (_index != 100)
                {
                    _index += 10;
                }
                AllGames();
                break;
            case 2:
                if (_index != 0)
                {
                    _index -= 10;
                }
                AllGames();
                break;
            case 3:
                gameInfo.ShowGame(gameIDs[0]);
                AllGames();
                break;
            case 4:
                gameInfo.ShowGame(gameIDs[1]);
                AllGames();
                break;
            case 5:
                gameInfo.ShowGame(gameIDs[2]);
                AllGames();
                break;
            case 6:
                gameInfo.ShowGame(gameIDs[3]);
                AllGames();
                break;
            case 7:
                gameInfo.ShowGame(gameIDs[4]);
                AllGames();
                break;
            case 8:
                gameInfo.ShowGame(gameIDs[5]);
                AllGames();
                break;
            case 9:
                gameInfo.ShowGame(gameIDs[6]);
                AllGames();
                break;
            case 10:
                gameInfo.ShowGame(gameIDs[7]);
                AllGames();
                break;
            case 11:
                gameInfo.ShowGame(gameIDs[8]);
                AllGames();
                break;
            case 12:
                gameInfo.ShowGame(gameIDs[9]);
                AllGames();
                break;
        }
    }
}