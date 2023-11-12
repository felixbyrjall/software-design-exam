using DigitalGameStore.Model;

namespace DigitalGameStore.Interfaces; 

public interface IGameGenreRepo {
    public List<GameObject> RecommendGames();

    public List<int> GetIntGenres();
}