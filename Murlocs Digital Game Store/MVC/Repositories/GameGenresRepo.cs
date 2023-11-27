using NextGaming.Interfaces;
using NextGaming.Model;
using System.Collections.Generic;

namespace NextGaming.Repo;

public class GameGenresRepo : IGameGenreRepo
{
    private readonly Context _context;

    public GameGenresRepo(Context context)
    {
        _context = context;
    }
    public List<int> GetIntGames()
    {
        var interestedGameIds = (
            from interest in _context.Interest
            select interest.GameID).ToList();

        return interestedGameIds;
    }
    
    public List<int> GetIntGenres()
    {
        var interestedGameIds = GetIntGames();
        var interestedGenreIds = (
            from gameGenres in _context.GameGenres
            where interestedGameIds.Contains(gameGenres.GameID)
            select gameGenres.GenreID).ToList();

        return interestedGenreIds;
    }

    public int CompareGenres(List<int> gameGenresIds)
    {
        var interestedGenreIds = GetIntGenres();

        return gameGenresIds.Count(genreId => interestedGenreIds.Contains(genreId));
    }

    public int ScoreGame(Game game)
    {
		int score = 0;
		const int matchingScore = 100;

		var interestedGenreIds = GetIntGenres();
        var gameGenreIds = _context.GameGenres
            .Where(gameGenres => gameGenres.GameID == game.ID)
            .Select(gameGenres => gameGenres.GenreID)
            .ToList();
        int matchingGenreCount = CompareGenres(gameGenreIds);

        foreach (var genreId in gameGenreIds)
        {
            score += interestedGenreIds.Count(id => id == genreId) * matchingScore;
        }
        return score;
    }

    public List<GameObject> RecommendGames()
    {
        {
            var interestedGameIds = _context.Interest
                .Select(i => i.GameID)
                .ToList();

            var allGames = _context.Game
                .Where(g => !interestedGameIds.Contains(g.ID))
                .ToList();

            var recommendedGames = allGames.Select(game => new GameObject(game.ID, game.Name, ScoreGame(game)))
                                           .Where(gameObject => gameObject.Score > 0)
                                           .OrderByDescending(gameObject => gameObject.Score)
                                           .Take(5)
                                           .ToList();

            return recommendedGames;
        }
    }
}