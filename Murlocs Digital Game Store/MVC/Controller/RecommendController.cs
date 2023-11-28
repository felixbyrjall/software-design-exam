using NextGaming.Interfaces;
using NextGaming.Model;

namespace NextGaming.Controller; 

public class RecommendController
{
	#region Fields and dependencies
	private List<GameObject> _recommendedGames = new();

	private readonly IGameGenreRepo _gameGenreRepo;
    private readonly InterestController _interestController;

	public RecommendController(IGameGenreRepo gameGenreRepo, InterestController interestController)
    {
        _gameGenreRepo = gameGenreRepo;
        _interestController = interestController;
    }
	#endregion

	#region Methods for listing recommendations, adding and viewing games
	public List<string> GetRecommendedGameWithOptions()
    {
        List<string> options = new List<string> { "Back to main menu", "------------"};
        var totalGenresInInterstList = _gameGenreRepo.GetIntGenres().Count();
        if (totalGenresInInterstList != 0) 
        {
	        foreach (var game in _recommendedGames) {
		        options.Add("ID: " + game.ID + " Name: " + game.Name + " Match: " +
		                    (game.Score / totalGenresInInterstList).ToString() + "%");
	        }
        }
        return options;
    }

    public void ListRecommendedGames()
    {
        var recommendedGames = _gameGenreRepo.RecommendGames();
        _recommendedGames.Clear();
        AddRecommendedGamesList(recommendedGames);
    }

    private void AddRecommendedGamesList(List<GameObject> games)
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
	#endregion
}