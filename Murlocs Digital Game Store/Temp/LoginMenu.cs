using System.Data;
using System.Runtime.CompilerServices;
using DB;
using DigitalGameStore.Admin;
using DigitalGameStore.DB;
using DigitalGameStore.InterestList;
using DigitalGameStore.RecommendGames;
using DigitalGameStore.UI;
using DigitalGameStore.Users.Customer;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.Login;

public class LoginMenu {
    private Context _context;

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
				BrowseMenu(); // On enter --> Add game to interest list or read more about game or back
				break;
            case 1:
				InterestList(); // Browse your games (edit/delete) --> add more games --> BrowseMenu()
                break;
			case 2:
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

        AddGame addGame = new AddGame();
        DeleteGame deleteGame = new DeleteGame();
        DisplayList displayList = new DisplayList();

        string prompt = "(Use the arrows to select an option)";
        string[] options = { "Display List", "Add Interest", "Delete Interest", "Display All Games", "Exit" };
        Menu mainMenu = new Menu(prompt, options);

        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {

            case 0:
                Console.WriteLine("Here is your Interest List: ");
                displayList.DisplayInterest();
                break;
            case 1:
                Console.WriteLine("What game would you like to add from your interest list?: ");
                int addInput = int.Parse(Console.ReadLine());
                addGame.Add(addInput);
                InterestList();
                break;
            case 2:
                Console.WriteLine("What game would you like to delete from your interest list?: ");
                int deleteInput = int.Parse(Console.ReadLine());
                deleteGame.Delete(deleteInput);
                InterestList();
                break;
            case 3:
                displayList.DisplayAllGames();
                break;
            case 4:
                Environment.Exit(0);
                break;

        }
    }

    public async Task RecommendGamesAsync()
    {
        using (var context = new Context()) // Use the correct context class name
        {
            var interestAnalyzer = new InterestAnalyzer(context);
            var gameRecommender = new GameRecommender(context);

            var recommendedGames = await gameRecommender.RecommendGames(interestAnalyzer);

            foreach (var game in recommendedGames)
            {
                Console.WriteLine($"{game.Name} - Score: {game.Score}");
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }

    // Make sure to call this method asynchronously from the menu option.
    public void RecommendGames()
    {
        RecommendGamesAsync().GetAwaiter().GetResult(); // This will synchronously wait for the async operation.
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
