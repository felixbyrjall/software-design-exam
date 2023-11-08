using DigitalGameStore.Views;
using DigitalGameStore.Model;
using DigitalGameStore.MVC.Views;


namespace DigitalGameStore.Controller
{
    public class ListGamesController
    {
        private readonly GameModel _gameModel; // Kobler seg til Modellen, ergo Middleman
        private readonly BrowseView _browseView; // Kobler seg til View, ergo Middleman
        private static int _currentPage = 10;

        public ListGamesController(GameModel gameModel, BrowseView browseView)
        {
            _gameModel = gameModel;
            _browseView = browseView;
        }
        
        public void StartPage()
        {
            ListGames(_currentPage);
        }

        public void NextPage()
        {
            if (_currentPage != 100)
            {
                _currentPage += 10;
            }

            ListGames(_currentPage); 
        }
        
        public void PreviousPage()
        {
            if (_currentPage != 10)
            {
                _currentPage -= 10;
            }

            ListGames(_currentPage);   
        }

        public void ListGames(int pageIndex)
        {
            var games = _gameModel.GetAllGames((_currentPage - 9), _currentPage); // Henter spill fra GetAllGames metoden inne i GameModel
            _browseView.DisplayGameList(games); // Etter å ha fått spillene, kaller den på metoden inne i view for å display.
        }
    }
}
