namespace NextGaming.Model;

public class Game
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;
    public int Score { get; set; } // Hold the score for interestlist

    public int PublisherID { get; set; }
    public Publisher? Publisher { get; set; }

    public ICollection<Interest>? Interests { get; set; }
}