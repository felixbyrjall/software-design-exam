using DB;
using DigitalGameStore.DB;
using DigitalGameStore.UI;

namespace DigitalGameStore.InterestList;

public class DisplayList {

    public void DisplayIntrests() {

        List<String> gameSelections = new List<String>();
        gameSelections.Add("Previous Menu");
        using Context database = new();

        IList<Interest> interests = database.Interest.ToList();
        IList<Game> games = database.Game.ToList();

        var gamesInterest =
            (from Game in games
                join Interest in interests on Game.Game_Id equals Interest.GameID
                select new { GameName = Game.Name, GameID = Game.Game_Id }).ToList();
        foreach (var item in gamesInterest)
        {

            gameSelections.Add("Game ID: " + item.GameID + " Game Name: " + item.GameName);
        }
        
        string prompt = "(Use the arrows to select an option)";
        string[] options = gameSelections.ToArray();
        Menu mainMenu = new Menu(prompt, options);

        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {

            case 0:
                break;
        }
    }
}
