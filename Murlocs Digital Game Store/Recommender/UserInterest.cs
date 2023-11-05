using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalGameStore.Recommender
{
    public class UserInterest
    {
        // DB connection

        public async Task AddGameToIntList(int gameId)
        {
            // Add gameId to Interest_List in the database, using down/up arrow + enter.
            // Return interest_list.
        }

        public async Task<List<int>> GetUserInterestedGenreIds()
        {
            // Fetch list of game IDs interested in
            var interestedGameIds = new List<int>(); // DB Logic - Interested Games.

            // For each of those game IDs, find associated genres.
            var interestedGenreIds = new List<int>(); // DB Logic - Interested Genres.

            // Do not remove duplicates, because we rank by frequency
            return interestedGenreIds;
        }

        public async Task<int> CountMatchingGenres(List<int> gameGenreIds)
        {
            // Get the interested genre IDs.
            var interestedGenreIds = await GetUserInterestedGenreIds();

            // Count how many of the game's genres match the interests.
            // Can be done using LINQ to compare two lists and count matching elements.
            return gameGenreIds.Count(genreId => interestedGenreIds.Contains(genreId));
        }
    }
}

