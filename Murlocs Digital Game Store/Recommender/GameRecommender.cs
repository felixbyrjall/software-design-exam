using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalGameStore.DB;

namespace DigitalGameStore.Recommender
{
   /* public class GameRecommender
    {
        // List all games, call to db to retrieve all games
        public async Task<List<Game>> GetAllGames() {

            return new List<Game>();
        }

        // The Scoring system
        private int ScoreGame(Game game, UserInterest userInterest) 
        {
            int score = 0;
            int matchingGenreCount = userInterest.CountMatchingGenres(game.GenreID);

            score += matchingGenreCount * 10;


             PUBLISHER SCORING 
            if (userInterest.InterestedPublisher(game.PublisherId))
            {
                score += 10;
            } 

            return score; 
        }

        // Creating the Recommended Game List.
        public async Task<List<Game>> RecommendGames(UserInterest userInterest)
        {   
            var allGames = await GetAllGames();
            var recommendedGames = new List<Game>();

            foreach (var game in allGames)
            {
                var score = ScoreGame(game, userInterest);

                game.Score = score; 

                if (score > 0)
                {
                    recommendedGames.Add(game);
                }
            }

            return recommendedGames; // Show top 5. 
        }
       
    }*/
    
}
