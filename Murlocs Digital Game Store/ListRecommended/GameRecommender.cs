using DigitalGameStore.DB;
using Microsoft.EntityFrameworkCore;
using DB;
using DigitalGameStore.UI;

namespace DigitalGameStore.RecommendGames;

public class GameRecommender
{

    // Scoring system when iterating through each game.
    private async Task<int> ScoreGame(Game game, InterestAnalyzer userInterest)
    {
        using Context database = new();
        int score = 0;
        var genreIds = await database.GameGenres
                                     .Where(gg => gg.GameID == game.ID)
                                     .Select(gg => gg.GenreID)
                                     .ToListAsync();
        int matchingGenreCount = await userInterest.CompareGenres(genreIds);

        score += matchingGenreCount * 10;

        return score;
    }
    
    // Creating the recommended list by iterating through all games and using the scoring system.
    public async Task RecommendGames(InterestAnalyzer userInterest)
    {
        using Context database = new();

        List<int> interestedGameIds = await database.Interest
                                            .Select(i => i.GameID)
                                            .ToListAsync();

        List<Game> allGames = await database.Game
                                            .Where(g => !interestedGameIds.Contains(g.ID))
                                            .ToListAsync();

        var recommendedGames = new List<(Game game, int score)>();

        foreach (var game in allGames)
        {
            int score = await ScoreGame(game, userInterest);
            game.Score = score; 

            if (score > 0)
            {
                recommendedGames.Add((game, score));
            }
        }

        var MatchingGames = recommendedGames.OrderByDescending(g => g.score)
                           .Take(5)
                           .Select(g => $"Game ID: {g.game.ID} Game Name: {g.game.Name} Matching Score: {g.score}")
                           .ToList();

        // Ref. Jokubas Menu-options
        string additionalText = "(Use the arrows to select an option)";
        string[] menuOptions = MatchingGames.ToArray();
        MenuLogic mainMenu = new MenuLogic(additionalText, menuOptions);

        int selectedIndex = mainMenu.Start();

        switch (selectedIndex)
        {
            case 0:
                break;
        }
    }

}
