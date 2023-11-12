using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using DigitalGameStore.Repo;
using DigitalGameStore.Views;

namespace DigitalGameStore.Controller; 

public class RecommendController {

	private int _currentPage = 10;
	private int _lastPage;
	private const int _firstPage = 10;

	private readonly IGameGenreRepo _gameGenreRepo;
    private readonly IGameRepo _gameRepo;
    private readonly RecommendView _recommendView;
    private readonly InterestController _interestController;

    public RecommendController(IGameGenreRepo gameGenreRepo, IGameRepo gameRepo, RecommendView recommendView, InterestController interestController){
        _gameGenreRepo = gameGenreRepo;
        _gameRepo = gameRepo;
        _recommendView = recommendView;
        _interestController = interestController;
    }

    private List<GameObject> _gamesOnPage = new();

    public List<string> GetRecommendedGameWithOptions() {
        
        List<string> options = new List<string> { "Back to main menu", "Next page", "Previous page", "------------"};
        var totalGenresInInterstList = _gameGenreRepo.GetIntGenres().Count(); 
        foreach (var game in _gamesOnPage)
        {
            options.Add("ID: " + game.ID + " Name: " + game.Name + " Match: " + (game.Score / totalGenresInInterstList).ToString() + "%");
        }
        return options;
    }
    public void ListRecommendedGames()
    {
        var recommendedGames = _gameGenreRepo.RecommendGames();
        _gamesOnPage.Clear();
        addRecommendedGamesList(recommendedGames);
    }

    private void addRecommendedGamesList(List<GameObject> games)
    {
        foreach (var game in games)
        {
            GameObject gameObject = new GameObject(game.ID, game.Name, game.Score);
            _gamesOnPage.Add(gameObject);
        }
    }

    public void GetSelectedGame(int gameId)
    {
        var game = _gameRepo.GetGameInfo(gameId);
        _recommendView.ShowGame(game);
    }

	public void Check(int i)
	{
		if (i == 1 && _currentPage != _lastPage)
		{
			_currentPage += 10;
		}
		else if (i == 2 && _currentPage != _firstPage)
		{
			_currentPage -= 10;
		}
        _interestController.ListNotInterested();
	}
}