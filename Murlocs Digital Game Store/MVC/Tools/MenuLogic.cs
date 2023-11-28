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
		TextColor("blue");
		Console.WriteLine(_additionalText);
		Console.ResetColor();

		for (int i = 0; i < _menuOptions.Length; i++)
		{
			string selectedOption = _menuOptions[i];
			string prefix;

			if(i == _selectedIndex)
			{
				prefix = "> ";
				TextColor("black");
				BgColor("white");
			}
			else
			{
				prefix = "  ";
				TextColor("white");
				BgColor("black");
			}
			Console.WriteLine($"{prefix}{selectedOption}");
		}
		Console.ResetColor();
		TextColor("green");
		Console.WriteLine(_notification);
		Console.ResetColor();
	}

	public int Start()
	{
		ConsoleKey keyPressed;

		do
		{
			Console.Clear();
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
		}while(keyPressed != ConsoleKey.Enter);

		return _selectedIndex;
	}

	public static void TextColor(string color)
	{
		try
		{
			Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
		}
		catch (Exception)
		{
			Console.ForegroundColor = ConsoleColor.Red; // If developer types in an invalid color (ex. typo), sets the text color to red for easy debugging
		}
	}

	public static void BgColor(string color)
	{
		try
		{
			Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
		}
		catch (Exception)
		{
			Console.BackgroundColor = ConsoleColor.Green; // If developer types in an invalid color (ex. typo), sets the background color to green for easy debugging
		}
	}
}