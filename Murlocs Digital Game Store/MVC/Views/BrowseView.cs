using DigitalGameStore.Controller;
using DigitalGameStore.Model;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;

namespace DigitalGameStore.Views
{
    public class BrowseView
    {

		private readonly BrowseController _browseController;

        public BrowseView(BrowseController browseController)
        {
            _browseController = browseController;
        }
        public void DisplayGameList(int i) 
        {
            var gameListWithOptions = _browseController.GetAllGamesWithOptions();

            _browseController.Check(i);
        }

		public void DisplayGames(int i)
		{
			DisplayGameList(i);
		}
	}
}
