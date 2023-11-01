using System.Threading.Channels;
using DigitalGameStore.Login;
using DigitalGameStore.UI;

namespace DigitalGameStore.Users.Employee;

public class EmployeeMenu {
    
    string title = @"
    __  ___           __                   ____  _       _ __        __   ______                        _____ __                
   /  |/  /_  _______/ /___  __________   / __ \(_)___ _(_) /_____ _/ /  / ____/___ _____ ___  ___     / ___// /_____  ________ 
  / /|_/ / / / / ___/ / __ \/ ___/ ___/  / / / / / __ `/ / __/ __ `/ /  / / __/ __ `/ __ `__ \/ _ \    \__ \/ __/ __ \/ ___/ _ \
 / /  / / /_/ / /  / / /_/ / /__(__  )  / /_/ / / /_/ / / /_/ /_/ / /  / /_/ / /_/ / / / / / /  __/   ___/ / /_/ /_/ / /  /  __/
/_/  /_/\__,_/_/  /_/\____/\___/____/  /_____/_/\__, /_/\__/\__,_/_/   \____/\__,_/_/ /_/ /_/\___/   /____/\__/\____/_/   \___/ 
                                               /____/                                                                           
";
		public void EmployeeOptions()
		{
			FindUser findUser = new FindUser();

			Console.WriteLine("Welcome " + findUser.GetUsername());
			string prompt = "(Use the arrows to select an option)";
			string[] options = { "List Products", "Sort Products", "Exit"};
			Menu mainMenu = new Menu(title, prompt, options);

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
