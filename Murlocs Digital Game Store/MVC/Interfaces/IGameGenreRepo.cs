using DigitalGameStore.Model;

namespace DigitalGameStore.Interfaces; 

public interface IGameGenreRepo {
    public List<string> RecommendGames();

}