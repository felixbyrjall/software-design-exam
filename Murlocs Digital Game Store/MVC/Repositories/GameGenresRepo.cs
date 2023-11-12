using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;

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
        return gameGenresIds.Count(genreId => interestedGenreIds.Contains(genreId));
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

    public List<string> RecommendGames() {
        {
            List<int> interestedGameIds = _context.Interest
                .Select(i => i.GameID)
                .ToList();
            List<Game> allGames = _context.Game
                .Where(g => !interestedGameIds.Contains(g.ID))
                .ToList();
            var recommendedGames = new List<(Game game, int score)>();

            foreach (var game in allGames) {
                int score = ScoreGame(game);
                game.Score = score;
                if (score > 0) {
                    recommendedGames.Add((game, score));
                }
            }
        
            List<string> matchingGames = recommendedGames.OrderByDescending(g => g.score)
                .Take(5)
                .Select(g => $"Game ID: {g.game.ID} Game Name: {g.game.Name} Matching Score: {g.score}")
                .ToList();
            Console.WriteLine(matchingGames);
            return matchingGames;

        }
        
    }
}