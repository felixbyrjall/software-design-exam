using NextGaming.Interfaces;
using NextGaming.Model;
using NextGaming.Tools;

namespace NextGaming.Views;

public class RecommendView
{
	private readonly GameInfoView _gameInfoView;

	public RecommendView(GameInfoView gameInfoView)
	{
		_gameInfoView = gameInfoView;
	}

	public void ShowGame(GameObject game)
    {
        Console.Clear();
        Console.WriteLine(_gameInfoView.ShowGameDetails(game));
		MenuLogic.TextColor("blue");
		Console.WriteLine("\nPress ENTER to continue");
		Console.ResetColor();
		Console.ReadLine();
	}
}