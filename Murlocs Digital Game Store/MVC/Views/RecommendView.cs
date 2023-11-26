using NextGaming.Interfaces;
using NextGaming.Model;
using NextGaming.Tools;

namespace NextGaming.Views;

public class RecommendView
{
	private readonly GameInfoView _gameDisplay;

	public RecommendView(GameInfoView gameDisplay)
	{
		_gameDisplay = gameDisplay;
	}

	public void ShowGame(GameObject game)
    {
        Console.Clear();
        Console.WriteLine(_gameDisplay.ShowGameDetails2(game));
		Func.TextColor("blue");
		Console.WriteLine("\nPress ENTER to continue");
		Console.ResetColor();
		Console.ReadLine();

	}
}