using DigitalGameStore.Controller;
using DigitalGameStore.Model;
using DigitalGameStore.Tools;
using DigitalGameStore.Views;
using DigitalGameStore.Repo;

namespace DigitalGameStore;

public class Program
{
    public static void Main(string[] args)
    {
        var context = new Context();
        var gameRepo = new GameRepo(context);
        var browseView = new BrowseView();
        var browseController = new BrowseController(gameRepo, browseView);

        var interestView = new InterestView();
        var gameObject = new GameObject();
        var interestRepo = new InterestRepo(context);
        var interestController = new InterestController(interestRepo, interestView, gameRepo);
        
        var recommendView = new RecommendView();
        var gameGenresRepo = new GameGenresRepo(context);
        var recommendController = new RecommendController(gameGenresRepo, gameRepo, recommendView);

        var menuTools = new MenuLogic(); 
        var menu = new Menu(menuTools, browseController, interestController, recommendController);

        // Run the main menu
        menu.MainMenu();
    }
}
