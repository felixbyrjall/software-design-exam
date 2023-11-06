using DigitalGameStore;
//using static System.Console;
namespace DigitalGameStore.UI
{
	public class Menu
	 {
		private int SelectedIndex;
		private string[] Options;
		private string Prompt;
			
		public Menu(string prompt, string[] options)
		{
			Prompt = prompt;
			Options = options;
			SelectedIndex = 0;
		}

		private void RenderDisplay()
		{
			Console.WriteLine(Prompt);

			for (int i = 0; i < Options.Length; i++)
			{
				string currentOption = Options[i];
				string prefix;

				if(i == SelectedIndex)
				{
					prefix = ">";
					Func.TextColor("black");
					Func.BgColor("white");
				}
				else
				{
					prefix = " ";
					Func.TextColor("white");
					Func.BgColor("black");
				}

				Console.WriteLine($"{prefix}{currentOption}");
			}
			Console.ResetColor();
		}
		public int Run()
		{
			ConsoleKey keyPressed;
			do
			{
				Console.Clear();
				RenderDisplay();

				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				keyPressed = keyInfo.Key;

				//Update SelectedIndex based on arrow keys.

				if (keyPressed == ConsoleKey.UpArrow)
				{
					SelectedIndex--;
					if (SelectedIndex == -1)
					{
						SelectedIndex = Options.Length - 1;
					}
				}
				else if (keyPressed == ConsoleKey.DownArrow)
				{
					SelectedIndex++;
					if (SelectedIndex == Options.Length)
					{
						SelectedIndex = 0;
					}
				}

			} while (keyPressed != ConsoleKey.Enter);

			return SelectedIndex;
		}
	}
}

