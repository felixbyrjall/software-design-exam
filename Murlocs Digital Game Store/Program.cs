using DigitalGameStore.Controller;
using DigitalGameStore.Model;
using DigitalGameStore.Repo;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;

namespace DigitalGameStore;

public class Program
{
    public static void Main(string[] args)
    {
        var context = new Context();
        var gameRepo = new GameRepo(context);
        var browseController = new BrowseController(gameRepo);
        var browseView = new BrowseView(browseController);
        var menuTools = new MenuLogic(); 
        var menu = new Menu(browseView, menuTools, browseController);

        // Run the main menu
        menu.MainMenu();
    }
}
