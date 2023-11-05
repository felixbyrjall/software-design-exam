namespace DigitalGameStore.DB; 

public class GameGenres {
    
            public int GameGenres_Id { get; set; }
            
            public int Game_Id { get; set; }
            public Game? Games { get; set; }
            
            public int Genre_Id { get; set; }
            public Genre? Genres { get; set; }
}