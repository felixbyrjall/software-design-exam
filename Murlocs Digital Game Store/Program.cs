using NextGaming.Controller;
using NextGaming.Model;
using NextGaming.Tools;
using NextGaming.Views;
using NextGaming.Repo;

namespace NextGaming;

public class Program
{
    public static void Main(string[] args)
    {
        var context = new Context();
		var menuLogic = new MenuLogic();

		var gameRepo = new GameRepo(context);
		var gameGenresRepo = new GameGenresRepo(context);
		var interestRepo = new InterestRepo(context);

		var gameInfoView = new GameInfoView();
		var browseView = new BrowseView();
		var interestView = new InterestView();
		var recommendView = new RecommendView(gameInfoView);
		
		var browseController = new BrowseController(gameRepo, browseView, interestRepo, menuLogic, gameInfoView);
		var interestController = new InterestController(interestRepo, interestView, gameRepo, browseController, menuLogic, gameInfoView);
        var recommendController = new RecommendController(gameGenresRepo, gameRepo, recommendView, interestController, gameInfoView);
  
        var menuController = new MenuController(menuLogic, browseController, interestController, recommendController, interestRepo);

        // Run the main menu
        menuController.MainMenu();
    }
}
