using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB;

namespace DigitalGameStore.RecommendGames
{
    public class InterestAnalyzer
    {
        public async Task<List<int>> GetIntGames()
        {
            using Context database = new();
            var interestedGameIds = await database.Interest
                .Select(il => il.GameID)
                .ToListAsync();
            return interestedGameIds;
        }

        public async Task<List<int>> GetIntGenres()
        {
            using Context database = new();
            var interestedGameIds = await GetIntGames();
            var interestedGenreIds = await database.GameGenres
                .Where(gg => interestedGameIds.Contains(gg.GameID))
                .Select(gg => gg.GenreID)
                .ToListAsync();
            return interestedGenreIds;
        }

        public async Task<int> CompareGenres(List<int> gameGenreIds)
        {
            var interestedGenreIds = await GetIntGenres();
            return gameGenreIds.Count(genreId => interestedGenreIds.Contains(genreId));
        }
    }
}
