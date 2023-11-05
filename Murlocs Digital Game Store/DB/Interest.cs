namespace DigitalGameStore.DB; 

public class Interest {
    public int Interest_Id { get; set; }
    
    public int GameID { get; set; }
    public Game? Game { get; set; }
}