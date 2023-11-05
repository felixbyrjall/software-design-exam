namespace DigitalGameStore.DB; 

public class GameGenres {
    
            public int GameGenres_ID { get; set; }
            
            public int Game_Id { get; set; }
            public Game? Games { get; set; }
            
            public int Genre_ID { get; set; }
            public Genre? Genres { get; set; }
}