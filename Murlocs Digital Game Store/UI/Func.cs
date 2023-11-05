using System;
using Microsoft.Data.Sqlite;
using static System.Console;
namespace DigitalGameStore.UI
{
	public class Func
	{
		public SqliteConnection Connect() {
			SqliteConnection _sqliteConnection;
			_sqliteConnection = new SqliteConnection("Data source = Resources/DigitalGameStore.db");
			 _sqliteConnection.Open();
			 return Connect();
		}
		#region Console action
		public static string? _userInput;

		public static void Clear()
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
		#endregion

		#region Console style
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

		public static void PrintTitle()
		{
			string title = @"
    __  ___           __                   ____  _       _ __        __   ______                        _____ __                
   /  |/  /_  _______/ /___  __________   / __ \(_)___ _(_) /_____ _/ /  / ____/___ _____ ___  ___     / ___// /_____  ________ 
  / /|_/ / / / / ___/ / __ \/ ___/ ___/  / / / / / __ `/ / __/ __ `/ /  / / __/ __ `/ __ `__ \/ _ \    \__ \/ __/ __ \/ ___/ _ \
 / /  / / /_/ / /  / / /_/ / /__(__  )  / /_/ / / /_/ / / /_/ /_/ / /  / /_/ / /_/ / / / / / /  __/   ___/ / /_/ /_/ / /  /  __/
/_/  /_/\__,_/_/  /_/\____/\___/____/  /_____/_/\__, /_/\__/\__,_/_/   \____/\__,_/_/ /_/ /_/\___/   /____/\__/\____/_/   \___/ 
                                               /____/                                                                           
";
			Func.WriteOutput(title);
		}
		#endregion

	}
}

