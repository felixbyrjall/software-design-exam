using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalGameStore.DB;
using Microsoft.EntityFrameworkCore;
using DB;

namespace DigitalGameStore.RecommendGames
{
    public class GameRecommender : IGameRecommender
    {
        // Database connection
        private readonly Context _context;
        public GameRecommender(Context context)
        {
            _context = context;
        }

        // Fetch all games
        public async Task<List<Game>> GetAllGames()
        {
            return await _context.Game.ToListAsync();
        }

        // Scoring system when iterating through each game.
        private async Task<int> ScoreGame(Game game, InterestAnalyzer userInterest)
        {
            int score = 0;
            var genreIds = await _context.GameGenres
                                         .Where(gg => gg.Game_Id == game.Game_Id)
                                         .Select(gg => gg.Genre_Id)
                                         .ToListAsync();
            int matchingGenreCount = await userInterest.CompareGenres(genreIds);

            score += matchingGenreCount * 10;

            return score;
        }

        // Creating the recommended list by iterating through all games and using the scoring system.
        public async Task<List<Game>> RecommendGames(InterestAnalyzer userInterest)
        {
            var allGames = await GetAllGames();
            var recommendedGames = new List<Game>();

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
        Task<List<Game>> RecommendGames(InterestAnalyzer userInterest);
        // Interface to ensure the implementation of game recommendation method.
    }
}
