//using static System.Console;
namespace NextGaming.Tools;

	public class MenuLogic
	{
		private int _selectedIndex;
		private string[] _menuOptions;
		private string _additionalText;
		private string _notification;
			
		public MenuLogic(string additionalText, string[] menuOptions, int? currentIndex, string notification)
		{
			_additionalText = additionalText;
			_menuOptions = menuOptions;
			_notification = notification;
			if(currentIndex != null)
			{
				_selectedIndex = (int)currentIndex;
			}
			else
			{
				_selectedIndex = 0;
			}
		}

		public MenuLogic(string additionalText, string[] menuOptions)
		{
			_additionalText = additionalText;
			_menuOptions = menuOptions;
			_selectedIndex = 0;
		}

		public MenuLogic()
		{
		
		}

		public int CallMenu(string text, List<string> list, int? i, string notification)
		{
			MenuLogic mainMenu = new MenuLogic(text, list.ToArray(), i, notification);
			int selectedIndex = mainMenu.Start();

			return selectedIndex;
		}


	private void ShowMenu()
		{
		Func.TextColor("blue");
		Func.WriteOutput(_additionalText);
		Console.ResetColor();

			for (int i = 0; i < _menuOptions.Length; i++)
			{
				string selectedOption = _menuOptions[i];
				string prefix;

				if(i == _selectedIndex)
				{
					prefix = "> ";
					Func.TextColor("black");
					Func.BgColor("white");
				}
				else
				{
					prefix = "  ";
					Func.TextColor("white");
					Func.BgColor("black");
				}

				Func.WriteOutput($"{prefix}{selectedOption}");
			}
			Console.ResetColor();
			Func.TextColor("green");
			Func.WriteOutput(_notification);
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
					_selectedIndex--;
					if (_selectedIndex == -1)
					{
						_selectedIndex = _menuOptions.Length - 1;
					}
				}
				else if (keyPressed == ConsoleKey.DownArrow)
				{
					_selectedIndex++;
					if (_selectedIndex == _menuOptions.Length)
					{
						_selectedIndex = 0;
					}
				}

			} while (keyPressed != ConsoleKey.Enter);

			return _selectedIndex;
		}
	}

