using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalGameStore.DB;
using Microsoft.EntityFrameworkCore;

namespace DigitalGameStore.RecommendGames
{
    public class GameRecommender : IGameRecommender
    {
        // Database connection
        private readonly GameContext _context;
        public GameRecommender(GameContext context)
        {
            _context = context;
        }

        // Fetch all games
        public async Task<List<Games>> GetAllGames()
        {
            return await _context.Games.ToListAsync();
        }

        // Scoring system when iterating through each game.
        private async Task<int> ScoreGame(Games game, InterestAnalyzer userInterest)
        {
            int score = 0;
            var genreIds = await _context.Game_Genres
                                         .Where(gg => gg.Game_id == game.Game_id)
                                         .Select(gg => gg.Genre_id)
                                         .ToListAsync();
            int matchingGenreCount = await userInterest.CompareGenres(genreIds);

            score += matchingGenreCount * 10;

            return score;
        }

        // Creating the recommended list by iterating through all games and using the scoring system.
        public async Task<List<Games>> RecommendGames(InterestAnalyzer userInterest)
        {
            var allGames = await GetAllGames();
            var recommendedGames = new List<Games>();

            foreach (var game in allGames)
            {
                var score = await ScoreGame(game, userInterest);

                game.Score = score;  //  'Score' property of Games table.

                if (score > 0)
                {
                    recommendedGames.Add(game);
                }
            }

            return recommendedGames
                   .OrderByDescending(g => g.Score)
                   .Take(5)  // Show top 5.
                   .ToList();
        }
    }

    public interface IGameRecommender
    {
        Task<List<Games>> RecommendGames(InterestAnalyzer userInterest);
        // Interface to ensure the implementation of game recommendation method.
    }
}
