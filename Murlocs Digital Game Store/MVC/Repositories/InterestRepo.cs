using DigitalGameStore.MVC.Controller;
using NextGaming.Interfaces;
using NextGaming.Model;

namespace NextGaming.Repo;

public class InterestRepo : IInterestRepo
{
    private readonly Context _context;
    private readonly NotificationController _notificationController;

    public InterestRepo(Context context, NotificationController notificationController)
    {
        _context = context;
        _notificationController = notificationController;
    }

    public List<GameObject> GetNotInterestedGames()
    {
		List<GameObject> list = new List<GameObject>();

		var notInterestedList =
            (from Game in _context.Game
             from Interest in _context.Interest.Where(mapping => mapping.GameID == Game.ID).DefaultIfEmpty()
			 where Interest.ID == null
			 select new { GameName = Game.Name, GameID = Game.ID });
        
        foreach (var item in notInterestedList)
        {
            GameObject gameObject = new GameObject(item.GameID, item.GameName);
            list.Add(gameObject);
        }
        return list;
    }

    public int CountGamesNotInInterestList() // Count all games in catalogue
    {
        var allGames = _context.Game.Count();
        var interestList = _context.Interest.Count();

        return allGames - interestList;
    }

	public int CountGamesInInterestList() // Count all games in catalogue
	{
		var interestList = _context.Interest.Count();

		return interestList;
	}
	
	public void AddGameToInterest(int gameId)
    {
        Interest newInterest = new()
        {
            GameID = gameId
        };

        _context.Interest.Add(newInterest);
        _context.SaveChanges();
        _notificationController.OnChange(gameId, "add");
    }

    public void RemoveGameFromInterest(int gameId)
    {
        var findInterest = _context.Interest.FirstOrDefault(g => g.GameID == gameId);
        if (findInterest != null)
        {
			_context.Interest.Remove(findInterest);
			_context.SaveChanges();
			_notificationController.OnChange(gameId, "remove");
		}
    }

	public List<GameObject> GetGamesOnInterestList(int page)
	{
		List<GameObject> list = new List<GameObject>();

		var interestList =
            (from game in _context.Game
             from interest in _context.Interest.Where(mapping => mapping.GameID == game.ID).DefaultIfEmpty()
             where interest.ID != null
             select new { GameName = game.Name, GameID = game.ID }).Skip(page - 10).Take(10);
		
		foreach (var item in interestList)
		{
			GameObject gameObject = new GameObject(item.GameID, item.GameName);
			list.Add(gameObject);
		}
		return list;
	}
}