using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB;

namespace DigitalGameStore.RecommendGames
{
    public class InterestAnalyzer : IInterestAnalyzer
    {
        private readonly Context _context;

        public InterestAnalyzer(Context context)
        {
            _context = context;
        }
        
        public async Task<List<int>> DisplayWithLINQ() {
        
            var gameList = await _context.Game
                .Select(g => g.Game_Id)
                .ToListAsync();
            return gameList;
        }

        public async Task<List<int>> GetIntGames()
        {
            var interestedGameIds = await _context.Interest
                .Select(il => il.GameID)
                .ToListAsync();
            return interestedGameIds;
        }

        public async Task<List<int>> GetIntGenres()
        {
            var interestedGameIds = await GetIntGames();
            var interestedGenreIds = await _context.GameGenres
                .Where(gg => interestedGameIds.Contains(gg.Game_Id))
                .Select(gg => gg.Genre_Id)
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
