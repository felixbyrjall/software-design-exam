namespace NextGaming.Model;

public class GameGenres
{    
    public int ID { get; set; }
            
    public int GameID { get; set; }
    public Game? Games { get; set; }
            
    public int GenreID { get; set; }
    public Genre? Genres { get; set; }
}