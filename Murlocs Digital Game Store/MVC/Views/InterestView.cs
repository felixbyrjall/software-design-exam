using DigitalGameStore.Model;
using DigitalGameStore.Tools;
namespace DigitalGameStore.Views; 

public class InterestView
{
    public void ShowGame(GameObject game)
    {
        GameDisplay.ShowGameDetails(game);
    }
}