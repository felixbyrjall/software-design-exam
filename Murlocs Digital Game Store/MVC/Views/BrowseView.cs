using DigitalGameStore.Controller;
using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;

namespace DigitalGameStore.Views
{
    public class BrowseView
    {
		public void ShowGame(GameObject game)
		{
			Console.WriteLine("Name: " + game.Name +
							  "\n Publisher: " + game.Publisher +
							  "\n Release: " + game.Release +
							  "\n Genres: " + game.Genres);
		}

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
}
