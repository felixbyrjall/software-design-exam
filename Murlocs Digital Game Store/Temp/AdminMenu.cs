using DigitalGameStore.Login;
using DigitalGameStore.UI;

namespace DigitalGameStore.Admin;

public class AdminMenu
{
    public void AdminOptions()
    {

        FindUser findUser = new FindUser();

        Console.WriteLine("Welcome " + findUser.GetUsername());
        string prompt = "(Use the arrows to select an option)";
        string[] options = { "Add Product", "Delete Product", "Exit" };
        Menu mainMenu = new Menu(prompt, options);

        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Add add = new Add();
                add.addMenu();
                AdminOptions();
                break;
            case 1:
                Delete delete = new Delete();
                delete.deleteMenu();
                break;
            case 2:
                Environment.Exit(0);
                break;
        }
    }
}
