using NextGaming.Interfaces;
using NextGaming.Model;
using NextGaming.Repo;
using NextGaming.Tools;
using NextGaming.Views;

namespace NextGaming.Controller; 

public class RecommendController {

	private int _currentPage = 10;
	private int _lastPage;
	private const int _firstPage = 10;

	private readonly IGameGenreRepo _gameGenreRepo;
    private readonly IGameRepo _gameRepo;
    private readonly RecommendView _recommendView;
    private readonly InterestController _interestController;
	private readonly GameInfoView _gameDisplay;

	public RecommendController(IGameGenreRepo gameGenreRepo, IGameRepo gameRepo, RecommendView recommendView, InterestController interestController, GameInfoView gameDisplay){
        _gameGenreRepo = gameGenreRepo;
        _gameRepo = gameRepo;
        _recommendView = recommendView;
        _interestController = interestController;
        _gameDisplay = gameDisplay;
    }

    private List<GameObject> _recommendedGames = new();

    public List<string> GetRecommendedGameWithOptions() {
        
        List<string> options = new List<string> { "Back to main menu", "------------"};
        var totalGenresInInterstList = _gameGenreRepo.GetIntGenres().Count(); 
        foreach (var game in _recommendedGames)
        {
            options.Add("ID: " + game.ID + " Name: " + game.Name + " Match: " + (game.Score / totalGenresInInterstList).ToString() + "%");
        }
        return options;
    }
    public void ListRecommendedGames()
    {
        var recommendedGames = _gameGenreRepo.RecommendGames();
        _recommendedGames.Clear();
        addRecommendedGamesList(recommendedGames);
    }

    private void addRecommendedGamesList(List<GameObject> games)
    {
        foreach (var game in games)
        {
            GameObject gameObject = new GameObject(game.ID, game.Name, game.Score);
            _recommendedGames.Add(gameObject);
        }
    }

	public void GetSelectedGameFromRecommendMenu(int gameId)
	{
		int currentGameId = _recommendedGames[gameId].ID;
		_interestController.GetSelectedGameFromRecommendMenu(currentGameId);
	}
}