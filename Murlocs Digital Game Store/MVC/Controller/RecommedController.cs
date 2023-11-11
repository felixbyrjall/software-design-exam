using DigitalGameStore.Model;
using DigitalGameStore.MVC.Repositories;
using DigitalGameStore.Tools;
using Microsoft.EntityFrameworkCore;

namespace DigitalGameStore.Controller; 

public class RecommedController { 
    private List<String> _gamesOnPage = new();
    private int _countGames;
    private int _currentPage = 10;
    private const int _lastPage = 100;
    private const int _firstPage = 10;

    public void addGamesToMenu() {
  
    }
    
    public List<string> GetRecommendedGameWithOptions() {
        GameGenresRepo s = new();
        List<string> options = s.RecommendGames();
    }
    
}