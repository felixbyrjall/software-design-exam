namespace NextGaming.Model;

public class ChangeArgs : EventArgs
{
    public int gameId { get; set; }
    public string method { get; set; }
}