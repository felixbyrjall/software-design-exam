using DigitalGameStore.Models;
using DigitalGameStore.MVC.Views;

namespace DigitalGameStore.MVC.Controllers
{
    public class ListGamesController
    {
        private readonly GameModel _gameModel;
        private readonly BrowseView _browseView;

        public ListGamesController(GameModel gameModel, BrowseView browseView)
        {
            _gameModel = gameModel;
            _browseView = browseView;
        }

        public void ListGames(int page)
        {
            const int PageSize = 10;
            var games = _gameModel.GetAllGames(page, PageSize);
            _browseView.DisplayGameList(games, page, PageSize);
        }
    }
}
