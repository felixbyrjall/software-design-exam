using DB;
using DigitalGameStore.DB;

namespace DigitalGameStore.InterestList
{

	internal class AddGame
	{

		public void Add(int gameId)
		{

			Interest newInterest = new()
			{
				GameID = gameId
			};

			using Context database = new Context();
			database.Interest.Add(newInterest);
			database.SaveChanges();
		}
	}
}

/* Functionalities:
    1. Display interest_list table
    2. Browse games with up/down arrow
    3. Add games to interest list (trigger BrowseGames())
    4. Delete games
    5. Back to main menu
*/