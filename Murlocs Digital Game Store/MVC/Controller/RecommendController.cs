using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using DigitalGameStore.MVC.Repositories;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;
using Microsoft.EntityFrameworkCore;

namespace DigitalGameStore.Controller; 

public class RecommendController {
    private List<string> _gamesOnPage;
    private readonly GameGenresRepo _gameGenresRepo;
    private readonly GameRepo _gameRepo;
    private readonly RecommendView _recommendView;

    public RecommendController(GameGenresRepo gameGenresRepo, GameRepo gameRepo, RecommendView recommendView) {
        _gameGenresRepo = gameGenresRepo;
        _gameRepo = gameRepo;
        _recommendView = recommendView;
    }
    

    public List<string> GetRecommendedGameWithOptions() {
        
        List<string> options = new List<string>(_gameGenresRepo.RecommendGames());
        _gamesOnPage = options;
        return options;
    }
    public void GetSelectedGame(int gameId)
    {
        var game = _gameRepo.GetGameInfo(gameId);
        _recommendView.ShowGame(game);
    }
    
}