namespace DigitalGameStore.DB; 

public class InterestTable {
    public int Interest_ID { get; set; }
    
    public int GameID { get; set; }
    public Game? Game { get; set; }
}