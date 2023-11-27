namespace NextGaming.Model;

public class Publisher
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Game>? Games { get; set; }
}