using System;
namespace DigitalGameStore.UI
{
	public class Start
	{
		string title = @"
    __  ___           __                   ____  _       _ __        __   ______                        _____ __                
   /  |/  /_  _______/ /___  __________   / __ \(_)___ _(_) /_____ _/ /  / ____/___ _____ ___  ___     / ___// /_____  ________ 
  / /|_/ / / / / ___/ / __ \/ ___/ ___/  / / / / / __ `/ / __/ __ `/ /  / / __/ __ `/ __ `__ \/ _ \    \__ \/ __/ __ \/ ___/ _ \
 / /  / / /_/ / /  / / /_/ / /__(__  )  / /_/ / / /_/ / / /_/ /_/ / /  / /_/ / /_/ / / / / / /  __/   ___/ / /_/ /_/ / /  /  __/
/_/  /_/\__,_/_/  /_/\____/\___/____/  /_____/_/\__, /_/\__/\__,_/_/   \____/\__,_/_/ /_/ /_/\___/   /____/\__/\____/_/   \___/ 
                                               /____/                                                                           
";
		public void StartMenu()
		{
			Console.Title = "Murlocs Digital Game Store";
			RunMainMenu();
		}

		private void RunMainMenu()
		{
			string prompt = "(Use the arrows to select an option)";
			string[] options = { "Add", "Edit", "Remove", "List Products", "Sort Products", "Exit"};
			Menu mainMenu = new Menu(title, prompt, options);
			int selectedIndex = mainMenu.Run();

			switch (selectedIndex)
			{
				case 0:
					AddMenu();
					break;
				case 1:
					EditMenu();
					break;
				case 2:
					RemoveMenu();
					break;
				case 3:
					ListMenu();
					break;
				case 4:
					SortMenu();
					break;
				case 5:
					ExitMenu();
					break;
			}
		}
		private void AddMenu()
		{
			Console.Clear();
			Console.WriteLine(title);
			Console.WriteLine("ADD MENU");
			Console.WriteLine("Press any key to go back");
			Console.ReadKey(true);
			RunMainMenu();
		}


		private void EditMenu()
		{

		}

		private void RemoveMenu()
		{

		}

		private void ListMenu()
		{

		}

		private void SortMenu()
		{

		}

		private void ExitMenu()
		{
			Console.ReadKey(true);
			Environment.Exit(0);
		}
	}
}

