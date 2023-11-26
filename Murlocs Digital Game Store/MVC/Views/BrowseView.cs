using NextGaming.Model;
using NextGaming.Tools;
namespace NextGaming.Views;

public class BrowseView
{
	public void LoadingScreen(int time)
	{
		Func.WriteOutput("Loading.");
		Thread.Sleep(time);
		Func.Clear();
		Func.WriteOutput("Loading..");
		Thread.Sleep(time);
		Func.Clear();
		Func.WriteOutput("Loading...");
	}
}
