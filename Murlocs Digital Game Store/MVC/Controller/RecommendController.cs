using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using DigitalGameStore.Repo;
using DigitalGameStore.Views;

namespace DigitalGameStore.Controller; 

public class RecommendController {    
   
    private readonly IGameGenreRepo _gameGenreRepo;
    private readonly IGameRepo _gameRepo;
    private readonly RecommendView _recommendView;
        public RecommendController(IGameGenreRepo gameGenreRepo, IGameRepo gameRepo, RecommendView recommendView){
        _gameGenreRepo = gameGenreRepo;
        _gameRepo = gameRepo;
        _recommendView = recommendView;
    }

    private List<GameObject> _gamesOnPage = new();

    public List<string> GetRecommendedGameWithOptions() {
        
        List<string> options = new List<string> { "Back to main menu", "Next page", "Previous page", "------------"};
        foreach (var game in _gamesOnPage)
        {
            options.Add("ID: " + game.ID + " Name: " + game.Name + " Match: " + game.Score * 2+ "%");
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
}