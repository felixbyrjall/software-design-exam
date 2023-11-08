using DigitalGameStore.Views;
using DigitalGameStore.Model;


namespace DigitalGameStore.Controller
{
    public class ListGamesController
    {
        private readonly GameModel _gameModel; // Kobler seg til Modellen, ergo Middleman
        private readonly BrowseView _browseView; // Kobler seg til View, ergo Middleman

        public ListGamesController(GameModel gameModel, BrowseView browseView)
        {
            _gameModel = gameModel;
            _browseView = browseView;
        }

        public void ListGames()
        {
            var games = _gameModel.GetAllGames(); // Henter spill fra GetAllGames metoden inne i GameModel
            _browseView.DisplayGameList(games); // Etter å ha fått spillene, kaller den på metoden inne i view for å display.
        }
    }
}
