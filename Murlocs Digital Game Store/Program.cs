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
		var interestRepo = new InterestRepo(context);
		var menuLogic = new MenuLogic();
		var interestView = new InterestView();
        var gameDisplay = new GameDisplay();

		var recommendView = new RecommendView(gameDisplay);
		var gameGenresRepo = new GameGenresRepo(context);
		var browseController = new BrowseController(gameRepo, browseView, interestRepo, menuLogic, gameDisplay);
		var interestController = new InterestController(interestRepo, interestView, gameRepo, browseController, menuLogic, gameDisplay);
        var recommendController = new RecommendController(gameGenresRepo, gameRepo, recommendView, interestController, gameDisplay);

        
        var menu = new Menu(menuLogic, browseController, interestController, recommendController, interestRepo);

        // Run the main menu
        menu.MainMenu();
    }
}
