using System;
using static System.Console;
namespace DigitalGameStore.UI
{
	public class Func
	{
		public static string? _userInput;

		public static void ConsoleClear()
		{
			Clear();
		}

		public static string? ReadInput()
		{
			_userInput = ReadLine();
			return _userInput;
		}

		public static void WriteOutput(string s)
		{
			WriteLine(s);
		}

		public static void TextColor(string color)
		{
			try
			{
				ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
			} catch (Exception)
			{
				ForegroundColor = ConsoleColor.Red; // If developer types in an invalid color (ex. typo), sets the text color to red for easy debugging
			}
		}

		public static void BgColor(string color)
		{
			try
			{
				BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
			} catch (Exception)
			{
				BackgroundColor = ConsoleColor.Green; // If developer types in an invalid color (ex. typo), sets the background color to green for easy debugging
			}
		}

	}
}

