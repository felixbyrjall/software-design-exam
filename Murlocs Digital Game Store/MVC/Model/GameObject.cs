namespace DigitalGameStore.Model;

public class GameObject
{
    public int ID;
    public string Name;
    public string Publisher;
    public string ReleaseDate;
    public string Genres;


    public GameObject(int id, string name, string publisher, string releasedate, string genres)
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

    public GameObject()
    {

    }


}

