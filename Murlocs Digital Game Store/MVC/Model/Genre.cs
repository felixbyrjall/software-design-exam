namespace NextGaming.Model;

public class Genre
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<GameGenres>? GameGenres { get; set; }
}