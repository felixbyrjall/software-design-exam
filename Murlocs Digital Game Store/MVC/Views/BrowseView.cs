﻿using DigitalGameStore.Controller;
using DigitalGameStore.Model;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;

namespace DigitalGameStore.MVC.Views
{
    public class BrowseView
    {
        private static int currentIndex = 0;
        private static List<String> _gameName = new();
        private readonly ListGamesController _listGamesController;

        public BrowseView(ListGamesController listGamesController)
        {
            _listGamesController = listGamesController;
        }
        
        public void DisplayGameList(IEnumerable<Game> games) 
        {
            _gameName.Add("Back to Main Menu");
            _gameName.Add("Next 10 Games");
            _gameName.Add("Previous 10 Games");

            foreach (var game in games)
            {
                _gameName.Add("ID: " + game.ID+ " Name: " +game.Name);
            }
        }

        public void BrowseMenu() {
            string additionalText = "(Use the arrows to select an option)";
            string[] menuOptions = _gameName.ToArray();
            MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions, currentIndex);
            
            Menu menu = new Menu();
            var gameRepo = new Repo.GameRepo();
            var browseView = new BrowseView(_listGamesController);
            var listGamesController = new ListGamesController(gameRepo, browseView);

            int selectedIndex = mainMenu.Start();

            switch (selectedIndex)
            {
                case 0:
                    _gameName.Clear();
                    menu.ReturnToMainMenu();
                    break;
                case 1:
                    _gameName.Clear();
                    listGamesController.NextPage();
					currentIndex = 1;
					BrowseMenu();
                    break;
                case 2:
                    _gameName.Clear();
                    listGamesController.PreviousPage();
                    currentIndex = 2;
                    BrowseMenu();
                    break;
            }
        }
    }
}
