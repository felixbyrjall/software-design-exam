//using static System.Console;
namespace DigitalGameStore.Tools;

	public class MenuLogic
	 {
		private int SelectedIndex;
		private string[] MenuOptions;
		private string AdditionalText;
			
		public MenuLogic(string additionalText, string[] menuOptions)
		{
			AdditionalText = additionalText;
			MenuOptions = menuOptions;
			SelectedIndex = 0;
		}

		private void ShowMenu()
		{
			Func.WriteOutput(AdditionalText);

			for (int i = 0; i < MenuOptions.Length; i++)
			{
				string selectedOption = MenuOptions[i];
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

				Func.WriteOutput($"{prefix}{selectedOption}");
			}
			Console.ResetColor();
		}
		public int Start()
		{
			ConsoleKey keyPressed;
			do
			{
				Func.Clear();
				ShowMenu();

				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				keyPressed = keyInfo.Key;

				//Update SelectedIndex based on arrow keys.

				if (keyPressed == ConsoleKey.UpArrow)
				{
					SelectedIndex--;
					if (SelectedIndex == -1)
					{
						SelectedIndex = MenuOptions.Length - 1;
					}
				}
				else if (keyPressed == ConsoleKey.DownArrow)
				{
					SelectedIndex++;
					if (SelectedIndex == MenuOptions.Length)
					{
						SelectedIndex = 0;
					}
				}

			} while (keyPressed != ConsoleKey.Enter);

			return SelectedIndex;
		}
	}

