using DigitalGameStore.DB;

namespace DB; 

public class Publisher {
    public int Publisher_ID { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Game>? Games { get; set; }
}