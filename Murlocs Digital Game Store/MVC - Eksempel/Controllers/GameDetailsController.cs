using DigitalGameStore.Models;
using DigitalGameStore.MVC.Views;

namespace DigitalGameStore.MVC.Controllers
{
    public class GameDetailsController
    {
        private readonly GameModel _gameModel;
        private readonly GameInfoView _gameInfoView;

        public GameDetailsController(GameModel gameModel, GameInfoView gameInfoView)
        {
            _gameModel = gameModel;
            _gameInfoView = gameInfoView;
        }

        public void ShowGameDetails(int gameId)
        {
            var game = _gameModel.GetGameDetails(gameId);
            if (game != null)
            {
                _gameInfoView.DisplayGameDetails(game);
            }
            else
            {
                _gameInfoView.DisplayError("Game not found.");
            }
        }
    }
}
