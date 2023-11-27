using NextGaming.Model;

namespace NextGaming.Interfaces; 

public interface IGameGenreRepo
{
    public List<GameObject> RecommendGames();

    public List<int> GetIntGenres();
}