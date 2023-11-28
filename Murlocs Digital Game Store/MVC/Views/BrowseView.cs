using NextGaming.Model;
using NextGaming.Tools;
namespace NextGaming.Views;

public class BrowseView
{
	public void LoadingScreen(int time)
	{
		Console.Clear();
		Console.WriteLine("Loading.");
		Thread.Sleep(time);
		Console.Clear();
		Console.WriteLine("Loading..");
		Thread.Sleep(time);
		Console.Clear();
		Console.WriteLine("Loading...");
	}
}
