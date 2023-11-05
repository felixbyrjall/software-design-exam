using System.Data;
using System.Runtime.CompilerServices;
using DB;
using DigitalGameStore.Admin;
using DigitalGameStore.UI;
using DigitalGameStore.Users.Customer;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.Login;

public class LoginMenu {

    public void LoginOptions()
    {
        RegisterUser registerUser = new RegisterUser();

        string prompt = "(Use the arrows to select an option)";
        string[] options = { "Browse Games", "Interest list", "Recommendations", "Exit" };
        Menu mainMenu = new Menu(prompt, options);

        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
				Func.Clear();
				BrowseMenu(); // On enter --> Add game to interest list or read more about game or back
				break;
            case 1:
				Func.Clear();
				InterestList(); // Browse your games (edit/delete) --> add more games --> BrowseMenu()
                break;
			case 2:
				Func.Clear();
				RecommendGames(); // Browse recommended games --> add to interestlist (Refreshes recommendedgameslist)
				break;
			case 3:
                Environment.Exit(0);
                break;
        }
    }

    public void BrowseMenu()
    {

    }

    public void InterestList()
    {

    }

    public void RecommendGames()
    {

    }

    /*public void LoginScreen() {

        FindUser findUser = new FindUser();
        CustomerMenu customer = new CustomerMenu();
        EmployeeMenu employee = new EmployeeMenu();
        AdminMenu admin = new AdminMenu();
        
        Console.WriteLine("Please Login:\n");

        Console.WriteLine("Username: ");
        string? usernameInput = Console.ReadLine();
        Console.WriteLine("Password: ");
        string? passwordInput = Console.ReadLine();

        findUser.userSearch(usernameInput, passwordInput);
        string userType = findUser.GetType();

        switch (userType) {

            case "Employee":
                employee.EmployeeOptions();
                break;
            case "admin":
                admin.AdminOptions();
                break;
            case "Customer":
                customer.CustomerOptions();
                break;
            default: // if the user types in the wrong username/password combo, this case returns an error message
				Console.WriteLine("The Username and Password given did not match any User in our database, please try again.");
				LoginScreen();
                break;
		}
    }*/
}
