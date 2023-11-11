using DigitalGameStore.Model;
using DigitalGameStore.Interfaces;
using DigitalGameStore.Repo;
using Microsoft.EntityFrameworkCore;

namespace DigitalGameStore.MVC.Repositories;

public class GameGenresRepo {
    
    private readonly Context _context;

    public List<int> GetIntGenres() {
        InterestRepo s = new InterestRepo(_context, new GameObject());
        var interestedGameIds = s.GetIntGames();
        var interestedGenreIds = (from gameGenres in _context.GameGenres
            where interestedGameIds.Contains(gameGenres.GameID)
            select new { gameGenres.GenreID });
        List<int> list = new List<int>();
        foreach (var item in interestedGenreIds) {
            list.Add(item.GenreID);
        }
        return list;
    }

    public int CompareGenres(List<int> gameGenresIds) {
        var interestedGenreIds = GetIntGenres();
        return gameGenresIds.Count(genreId => interestedGenreIds.Contains(genreId));
    }

    private int ScoreGame(Game game, GameGenresRepo gameGenresRepo) {
        int score = 0;
        var genreIds = _context.GameGenres
            .Where(gameGenres => gameGenres.GameID == game.ID)
            .Select(gameGenres => gameGenres.GenreID)
            .ToList();
        int matchingGenreCount = CompareGenres(genreIds);
        score += matchingGenreCount * 10;
        return score;
    }

    public List<string> RecommendGames(GameGenresRepo user) {
        List<int> interestedGameIds = _context.Interest
            .Select(i => i.GameID)
            .ToList();
        List<Game> allGames = _context.Game
            .Where(g => !interestedGameIds.Contains(g.ID))
            .ToList();
        
        var recommendedGames = new List<(Game game, int score)>();

        foreach (var game in allGames) {
            int score = ScoreGame(game, user);
            game.Score = score;
            if (score > 0) {
                recommendedGames.Add((game, score));
            }
        }
        var matchingGames = recommendedGames.OrderByDescending(g => g.score)
            .Take(5)
            .Select(g => $"Game ID: {g.game.ID} Game Name: {g.game.Name} Matching Score: {g.score}")
            .ToList();

        return matchingGames;
    }

}