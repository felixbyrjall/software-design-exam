namespace DigitalGameStore.DB; 

public class Genre {
    
    public int Genre_ID { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<GameGenres>? GameGenres { get; set; }

}