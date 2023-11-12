using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using System.Collections.Generic;

namespace DigitalGameStore.Repo;

public class GameGenresRepo : IGameGenreRepo {

    private readonly Context _context;

    public GameGenresRepo(Context context) {
        _context = context;
    }
    public List<int> GetIntGames() {
        var interestedGameIds = (
            from interest in _context.Interest
            select new {interest.GameID });
        List<int> list = new List<int>();
        foreach (var item in interestedGameIds) {
            list.Add(item.GameID);
        }
        return list;
    }
    
    public List<int> GetIntGenres() {
        var interestedGameIds = GetIntGames();
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
        return gameGenresIds.Count(interestedGenreIds.Contains);
    }

    public int ScoreGame(Game game) {
        int score = 0;
        var genreIds = _context.GameGenres
            .Where(gameGenres => gameGenres.GameID == game.ID)
            .Select(gameGenres => gameGenres.GenreID)
            .ToList();
        int matchingGenreCount = CompareGenres(genreIds);
        score += matchingGenreCount * 10;
        return score;
    }

    public List<GameObject> RecommendGames() {
        {
            var interestedGameIds = _context.Interest
                .Select(i => i.GameID)
                .ToList();

            var allGames = _context.Game
                .Where(g => !interestedGameIds.Contains(g.ID))
                .ToList();

            var recommendedGames = new List<GameObject>();

            foreach (var game in allGames) {
                int score = ScoreGame(game);
                game.Score = score;
                GameObject gameObject = new GameObject(game.ID, game.Name, game.Score);
                if (score > 0) {
                    recommendedGames.Add(gameObject);
                }
            }
            
            var sortedRecommendedGames = recommendedGames.OrderByDescending(game => game.Score).Take(5).ToList();

            return sortedRecommendedGames;
        }
    }
}