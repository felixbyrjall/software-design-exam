using DigitalGameStore.Controller;
using DigitalGameStore.DB;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;

namespace DigitalGameStore.MVC.Views
{
    public class AlternativBrowseView
    {
        private readonly ListGamesController _listGamesController;
        private List<String> _gameName;

        public AlternativBrowseView(ListGamesController listGamesController)
        {
            _listGamesController = listGamesController;
            _gameName = new List<String>();
        }

        public void DisplayGameList(IEnumerable<Game> games)
        {
            _gameName.Clear();
            _gameName.Add("Back to Main Menu");
            _gameName.Add("Next 10 Games");
            _gameName.Add("Previous 10 Games");

            foreach (var game in games)
            {
                _gameName.Add("ID: " + game.ID + " Name: " + game.Name);
            }
            BrowseMenu();
        }

        public void BrowseMenu()
        {
            string additionalText = "(Use the arrows to select an option)";
            string[] menuOptions = _gameName.ToArray();
            MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions);
            Menu menu = new Menu();

            var gameModel = new Model.GameModel();
            var browseView = new BrowseView(_listGamesController);
            var listGamesController = new ListGamesController(gameModel, browseView);

            int selectedIndex = mainMenu.Start();

            switch (selectedIndex)
            {
                case 0:
                    menu.MainMenu();
                    break;
                case 1:
                    listGamesController.NextPage();
                    break;
                case 2:
                    listGamesController.PreviousPage();
                    break;
            }
            _gameName.Clear();
        }
    }
}