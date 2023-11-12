using DigitalGameStore.Model;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views; 

public class RecommendView {
    public void ShowGame(GameObject game) {
        GameDisplay.ShowGameDetails(game);
    }
}