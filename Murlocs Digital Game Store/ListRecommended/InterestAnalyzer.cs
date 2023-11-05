using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalGameStore.RecommendGames
{
    public class InterestAnalyzer : IInterestAnalyzer
    {
        private readonly GameContext _context;

        public InterestAnalyzer(GameContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetIntGames()
        {
            var interestedGameIds = await _context.Interest_List
                .Select(il => il.Game_id)
                .ToListAsync();
            return interestedGameIds;
        }

        public async Task<List<int>> GetIntGenres()
        {
            var interestedGameIds = await GetIntGames();
            var interestedGenreIds = await _context.Game_Genres
                .Where(gg => interestedGameIds.Contains(gg.Game_id))
                .Select(gg => gg.Genre_id)
                .ToListAsync();
            return interestedGenreIds;
        }

        public async Task<int> CompareGenres(List<int> gameGenreIds)
        {
            var interestedGenreIds = await GetIntGenres();
            return gameGenreIds.Count(genreId => interestedGenreIds.Contains(genreId));
        }
    }

    public interface IInterestAnalyzer
    {
        Task<int> CompareGenres(List<int> gameGenreIds);
        /// Interface to ensure the implementation of genre comparison method.
    }
}
