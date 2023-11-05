using System;
using DigitalGameStore.Admin;
using DigitalGameStore.Login;

namespace DigitalGameStore.UI
{
	public class EmployeeMenu
	{
		public void EmployeeOptions()
		{

			FindUser findUser = new FindUser();

			Console.WriteLine("Welcome " + findUser.GetUsername());
			string prompt = "(Use the arrows to select an option)";
			string[] options = { "List Products", "Sort Products", "Exit" };
			Menu mainMenu = new Menu(prompt, options);

			int selectedIndex = mainMenu.Run();

			switch (selectedIndex)
			{
				case 0:
					ListMenu();
					break;
				case 1:
					SortMenu();
					break;
				case 2:
					ExitMenu();
					break;
			}
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
