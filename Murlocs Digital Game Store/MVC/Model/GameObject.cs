namespace NextGaming.Model;

public class GameObject
{
    public int ID;
    public string Name;
    public string Publisher;
    public string ReleaseDate;
    public List<string> Genres;
    public int Score;

    public GameObject(int id, string name, string publisher, string releasedate, List<string> genres)
    {
        ID = id;
        Name = name;
        Publisher = publisher;
        ReleaseDate = releasedate;
        Genres = genres;
    }

    public GameObject(int id, string name)
    {
        ID = id;
        Name = name;
    }

    public GameObject(int id, string name, int score)
    {
        ID = id;
        Name = name;
        Score = score;
    }

    public GameObject()
    {

    }

    public GameObject(int id, string name, string publisher, string releasedate, List<string> genres, int score)
    {
        ID = id;
        Name = name;
        Publisher = publisher;
        ReleaseDate = releasedate;
        Genres = genres;
        Score = score;
    }
}