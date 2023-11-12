using DigitalGameStore.Interfaces;
using DigitalGameStore.Views;

namespace DigitalGameStore.Controller; 

public class RecommendController {
    private List<string> _gamesOnPage = new();
    
    
    private readonly IGameGenreRepo _gameGenreRepo;
    private readonly IGameRepo _gameRepo;
    private readonly RecommendView _recommendView;
        public RecommendController(IGameGenreRepo gameGenreRepo, IGameRepo gameRepo, RecommendView recommendView){
        _gameGenreRepo = gameGenreRepo;
        _gameRepo = gameRepo;
        _recommendView = recommendView;
    }
    


    public List<string> GetRecommendedGameWithOptions() {
        
        List<string> options = new List<string>(_gameGenreRepo.RecommendGames());
        _gamesOnPage = options;
        return options;
    }
    public void GetSelectedGame(int gameId)
    {
        var game = _gameRepo.GetGameInfo(gameId);
        _recommendView.ShowGame(game);
    }
    
}