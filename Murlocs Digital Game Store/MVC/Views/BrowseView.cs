using DigitalGameStore.Controller;
using DigitalGameStore.Model;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;

namespace DigitalGameStore.Views
{
    public class BrowseView
    {
		public static List<String> _allGames = new();

		private BrowseController _browseController = new();
        
        public void DisplayGameList(int i) 
        {
			_allGames.Add("Back to Main Menu");
			_allGames.Add("Next 10 Games");
			_allGames.Add("Previous 10 Games");
			_browseController.Check(i);
        }

		public void DisplayGames(int i)
		{
			DisplayGameList(i);
		}
	}
}
