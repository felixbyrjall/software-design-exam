using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using DigitalGameStore.Tools;

namespace DigitalGameStore.Views;

public class RecommendView
{
	private readonly GameDisplay _gameDisplay;

	public RecommendView(GameDisplay gameDisplay)
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