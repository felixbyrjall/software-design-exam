using System.Data;
using System.Runtime.CompilerServices;
using DB;
using DigitalGameStore.UI;
using DigitalGameStore.Users.Customer;
using DigitalGameStore.Users.Employee;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.Login;

public class LoginMenu {

    string title = @"
    __  ___           __                   ____  _       _ __        __   ______                        _____ __                
   /  |/  /_  _______/ /___  __________   / __ \(_)___ _(_) /_____ _/ /  / ____/___ _____ ___  ___     / ___// /_____  ________ 
  / /|_/ / / / / ___/ / __ \/ ___/ ___/  / / / / / __ `/ / __/ __ `/ /  / / __/ __ `/ __ `__ \/ _ \    \__ \/ __/ __ \/ ___/ _ \
 / /  / / /_/ / /  / / /_/ / /__(__  )  / /_/ / / /_/ / / /_/ /_/ / /  / /_/ / /_/ / / / / / /  __/   ___/ / /_/ /_/ / /  /  __/
/_/  /_/\__,_/_/  /_/\____/\___/____/  /_____/_/\__, /_/\__/\__,_/_/   \____/\__,_/_/ /_/ /_/\___/   /____/\__/\____/_/   \___/ 
                                               /____/                                                                           
";
    
    public void LoginOptions() {
        RegisterUser registerUser = new RegisterUser();
        
        string prompt = "(Use the arrows to select an option)";
        string[] options = { "Login", "Sign up", "Exit"};
        Menu mainMenu = new Menu(title, prompt, options);

        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                LoginScreen();
                break;
            case 1:
                registerUser.CreateUser();
                break;
            case 2:
                Environment.Exit(0);
                break;
        }
    }

    public void LoginScreen() {

        FindUser findUser = new FindUser();
        CustomerMenu customer = new CustomerMenu();
        EmployeeMenu employee = new EmployeeMenu();
        
        Console.WriteLine("Welcome to the Murloc Digital Game Store!");
        Console.WriteLine("Please Login:\n");

        Console.WriteLine("Username: ");
        string usernameInput = Console.ReadLine();
        Console.WriteLine("Password: ");
        string passwordInput = Console.ReadLine();

        findUser.userSearch(usernameInput, passwordInput);
        string userType = findUser.GetType();

        switch (userType) {

            case "Employee":
                employee.EmployeeOptions();
                break;
            case "noe":
                employee.EmployeeOptions();
                break;
            case "Customer":
                customer.CustomerOptions();
                break;
        }
        
        Console.WriteLine("The Username and Password given did not match any User in our database, please try again.");
        LoginScreen();
    }
}
