using DigitalGameStore.Views;
using DigitalGameStore.Repo;
using DigitalGameStore.Model;

namespace DigitalGameStore.Controller
{
    public class BrowseController
    {
        private readonly BrowseView? _browseView; // Kobler seg til View, ergo Middleman

		private readonly GameRepo _gameRepo = new();
		public static int _currentPage = 10;


		private void AddGames(IEnumerable<Game> games)
        {
			foreach (var game in games)
			{
				BrowseView._allGames.Add("ID: " + game.ID + " Name: " + game.Name);
			}
		
        }

        public void ListGames()
        {
            var games = _gameRepo.GetAllGames((_currentPage - 9), _currentPage); // Henter spill fra GetAllGames metoden inne i GameRepo
            AddGames(games); // Etter å ha fått spillene, kaller den på metoden inne i view for å display.
        }

		public void Check(int i)
		{
			if(i == 1 && _currentPage != 100)
			{
				_currentPage += 10;
			}
			else if(i == 2 && _currentPage != 10)
			{
				_currentPage -= 10;
			}
			ListGames();
		}
	}
}
