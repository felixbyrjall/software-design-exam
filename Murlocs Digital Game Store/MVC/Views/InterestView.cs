using DigitalGameStore.Model;

namespace DigitalGameStore.Views; 

public class InterestView {
    public void ShowGame(GameObject game)
    {
        Console.WriteLine("Name: " + game.Name +
                          "\n Publisher: " + game.Publisher +
                          "\n Release: " + game.ReleaseDate +
                          "\n Genres: " + game.Genres);
    }
}