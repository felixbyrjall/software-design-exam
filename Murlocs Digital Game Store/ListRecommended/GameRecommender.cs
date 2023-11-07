using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalGameStore.DB;
using Microsoft.EntityFrameworkCore;
using DB;
using static System.Formats.Asn1.AsnWriter;

namespace DigitalGameStore.RecommendGames
{
    public class GameRecommender : IGameRecommender
    {
        // Database connection tested.
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
                                         .Where(gg => gg.GameID == game.ID)
                                         .Select(gg => gg.GenreID)
                                         .ToListAsync();
            int matchingGenreCount = await userInterest.CompareGenres(genreIds);

            score += matchingGenreCount * 10;

            return score;
        }
        

        // Creating the recommended list by iterating through all games and using the scoring system.
        public async Task<List<String>> RecommendGames(InterestAnalyzer userInterest)
        {
            List<Game> allGames = await _context.Game.ToListAsync();
            var recommendedGames = new List<(Game game, int score)>();

            foreach (var game in allGames)
            {
                int score = await ScoreGame(game, userInterest);
                game.Score = score;  //  'Score' property of Games table.

                if (score > 0)
                {
                    recommendedGames.Add((game, score));
                }
            }

            var MatchingGames = recommendedGames.OrderByDescending(g => g.score)
                               .Take(5)
                               .Select(g => $"Game ID: {g.game.ID} Game Name: {g.game.Name} Matching Score: {g.score}")
                               .ToList();

            return MatchingGames;
        }

    }

    public interface IGameRecommender
    {
        Task<List<String>> RecommendGames(InterestAnalyzer userInterest);
        // Interface to ensure the implementation of game recommendation method.
    }
}
