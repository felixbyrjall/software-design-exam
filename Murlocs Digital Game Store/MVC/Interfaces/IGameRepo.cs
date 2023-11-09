using DigitalGameStore.Model;

namespace DigitalGameStore.Repo
{
    public interface IGameRepo
    {
        IList<Game> GetAllGames(int start, int end);
    }
}
