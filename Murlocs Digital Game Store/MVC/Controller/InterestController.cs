using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using DigitalGameStore.Views;

namespace DigitalGameStore.Controller;

public class InterestController {

    private int _currentPage = 10;
    private const int _lastPage = 100;
    private const int _firstPage = 10;
    private readonly IInterestRepo _interestRepo;
    private readonly InterestView _interestView;
    private readonly GameObject _gameObject;

    public InterestController(IInterestRepo interestRepo, InterestView interestView, GameObject gameObject) {
        _interestRepo = interestRepo;
        _interestView = interestView;
        _gameObject = gameObject;
    }

    private List<GameObject> _gamesNotAdded = new();

    private void GamesNotAddedToList(List<GameObject> games)
    {
            foreach (var game in games)
            {
                GameObject gameObject = new GameObject(game.ID, game.Name);
                _gamesNotAdded.Add(gameObject);
            }
    }

    public void ListNotInterested()
    {
        var gamesInterested = _interestRepo.GetNotInterestedGames((_currentPage - 9), _currentPage);
        _gamesNotAdded.Clear();
        GamesNotAddedToList(gamesInterested); // Kaller på metoden AddGames for å legge til spill i _allGames feltet i view.
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
        ListNotInterested();
    }

    public List<string> GetGamesOnPageWithOptions()
    {
        List<string> options = new List<string> { "Back to main menu", "Next page", "Previous page", "---------" };
        foreach (var game in _gamesNotAdded)
        {
            options.Add("ID: " + game.ID + " Name: " + game.Name);
        }
        return options;
    }


    public int GetCurrentPage()
    {
        return _currentPage;
    }

    private void SetCurrentPage(int currentPage)
    {
        _currentPage = currentPage;
    }

    public void AddInterest(int gameId)
    {
        _interestRepo.AddGameToInterest(_gamesNotAdded[gameId].ID);
    }

    public void RemoveInterest(int gameId)
    {
        _interestRepo.RemoveGameFromInterest(gameId);
    }

    public void GetSelectedGame(int gameId)
    {
        var game = _interestRepo.GetGameInfo(_gamesNotAdded[gameId].ID);
        _interestView.ShowGame(game);
    }

}