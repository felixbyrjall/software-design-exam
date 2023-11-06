using DB;
using DigitalGameStore.DB;
using DigitalGameStore.Login;
using DigitalGameStore.UI;

namespace DigitalGameStore.InterestList;

public class DisplayList
{

	public void DisplayIntrests()
	{

		List<String> gameSelections = new List<String>();
		gameSelections.Add("Previous Menu");
		using Context database = new();

		IList<Interest> interests = database.Interest.ToList();
		IList<Game> games = database.Game.ToList();

		var gamesInterest =
			(from Game in games
			 join Interest in interests on Game.ID equals Interest.GameID
			 select new { GameName = Game.Name, GameID = Game.ID }).ToList();
		foreach (var item in gamesInterest)
		{

			gameSelections.Add("Game ID: " + item.GameID + " Game Name: " + item.GameName);
		}

		string additionalText = "(Use the arrows to select an option)";
		string[] menuOptions = gameSelections.ToArray();
		MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions);

		int selectedIndex = mainMenu.Start();

		switch (selectedIndex)
		{

			case 0:
				break;
		}
	}
}
