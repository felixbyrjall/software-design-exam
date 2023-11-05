using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalGameStore.Recommender
{
    public class AnalyzeInterest
    {
        // DB connection

        public async Task<List<int>> GetInterestedGenreIds()
        {
            // Fetch list of game IDs interested in
            var interestedGameIds = new List<int>(); // DB Logic - Interested Games.

            // For each of those game IDs, find associated genres.
            var interestedGenreIds = new List<int>(); // DB Logic - Interested Genres.

            // Do not remove duplicates, because we rank by frequency
            return interestedGenreIds;
        }

        public async Task<int> CompareGenres(List<int> gameGenreIds)
        {
            // Get the interested genre IDs.
            var interestedGenreIds = await GetInterestedGenreIds();

            // Count how many of the game's genres match the interests.
            // Can be done using LINQ to compare two lists and count matching elements.
            return gameGenreIds.Count(genreId => interestedGenreIds.Contains(genreId));
        }
    }
}

