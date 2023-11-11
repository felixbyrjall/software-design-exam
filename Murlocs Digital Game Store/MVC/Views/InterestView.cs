using DigitalGameStore.Model;

namespace DigitalGameStore.Views; 

public class InterestView {
    public void ShowGame(string name, string publisher, string releaseDate, string genre1, string genre2, string genre3, string genre4, string genre5)
    {
        Console.WriteLine("Name: " + name +
                          "\n Publisher: " + publisher +
                          "\n Release: " + releaseDate +
                          "\n Genres: " + genre1 + ", " + genre2 + ", " + genre3 + ", " + genre4 + ", " + genre5);
    }
}