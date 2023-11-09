using DigitalGameStore.Model;
namespace DigitalGameStore.Repo;

public class GameRepo
{
    private readonly Context _context;

    public GameRepo(Context context)
    {
        _context = context;
    }
    public IList<Game> GetAllGames(int start, int end)
    {
        return _context.Game.Where(g => g.ID >= start && g.ID <= end).ToList();
	}
}
