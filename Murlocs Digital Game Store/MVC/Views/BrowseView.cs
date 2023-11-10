using DigitalGameStore.Controller;
using DigitalGameStore.Interfaces;
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

		public void ShowGame(GameObject game)
		{
			Console.WriteLine("Name: " + game.Name +
							  "\n Publisher: " + game.Publisher +
							  "\n Release: " + game.Release +
							  "\n Genres: " + game.Genres);
		}
	}
}
