using DigitalGameStore.Interfaces;
using DigitalGameStore.Views;
using DigitalGameStore.Model;
namespace DigitalGameStore.Controller
{

    public class BrowseController
    {
        private List<String> _gamesOnPage = new();
        private int _currentPage = 10;
        private int _lastPage;
        private const int _firstPage = 10;
        private bool gamesLoaded = false;

        private readonly IGameRepo _gameRepo;
        private readonly BrowseView _browseView;

        public BrowseController(IGameRepo gameRepo, BrowseView browseView)
        {
            _gameRepo = gameRepo;
            _browseView = browseView;
        }

        public int GetCurrentPage()
        {
            return _currentPage;
        }

        private void SetCurrentPage(int currentPage)
        {
            _currentPage = currentPage;
        }

        public void Check(int i)
        {
            if (i == 1 && GetCurrentPage() != _lastPage)
            {
                int j = GetCurrentPage();
                SetCurrentPage(j += 10);
            }
            else if (i == 2 && GetCurrentPage() != _firstPage)
            {
                int j = GetCurrentPage();
                SetCurrentPage(j -= 10);
            }
            ListGames();
        }

        public void ListGames()
        {
            var totalTime = LoadingTime();
            if (gamesLoaded == false)
            {
                _browseView.LoadingScreen(totalTime);
                gamesLoaded = true;
            }

            var games = _gameRepo.GetGamesOnPage((GetCurrentPage() - 9), GetCurrentPage());
            _gamesOnPage.Clear();
            GetGamesOnPageWithOptions();
            AddGamesToMenu(games); // Kaller på metoden AddGames for å legge til spill i _allGames feltet i view.
        }

        public List<string> GetGamesOnPageWithOptions()
        {
            _lastPage = _gameRepo.CountAllGames();
            List<string> options = new List<string> { "Back to main menu", "Next page", "Previous page", "---------" };
            options.AddRange(_gamesOnPage);
            return options;
        }

        private void AddGamesToMenu(IEnumerable<Game> games)
        {
            foreach (Game game in games)
            {
                _gamesOnPage.Add("ID: " + game.ID + " Name: " + game.Name);
            }
        }

        public void GetSelectedGame(int gameId)
        {
            var game = _gameRepo.GetGameInfo(gameId);

            _browseView.ShowGame(game);
        }

        public int LoadingTime()
        {
            int totalLoadingTime = _gameRepo.CountAllGames() * 5;
            return totalLoadingTime;
        }

    }
}