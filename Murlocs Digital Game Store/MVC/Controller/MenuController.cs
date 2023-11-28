﻿using DigitalGameStore.MVC.Controller;
using NextGaming.Model;
using NextGaming.Controller;
using NextGaming.Views;
using NextGaming.Tools;
using NextGaming.Interfaces;
using NextGaming.Repo;

namespace NextGaming.Views;

public class MenuController
{
    private readonly MenuLogic _menuLogic;
	private readonly BrowseController _browseController;
	private readonly InterestController _interestController;
	private readonly RecommendController _recommendController;
	private readonly IInterestRepo _interestRepo;
	private readonly NotificationController _notificationController;

	private string _prompt = "(Use the arrows to select an option)";
	private string _notification = "";
	public int CurrentIndex = 0;

	public MenuController(MenuLogic menuTools, BrowseController browseController, InterestController interestController, RecommendController recommendController, IInterestRepo interestRepo, NotificationController notificationController)
    {
        _menuLogic = menuTools;
        _browseController = browseController;
		_interestController = interestController;
		_recommendController = recommendController;
		_interestRepo = interestRepo;
		_notificationController = notificationController;
    }

	public void MainMenu()
    {
		List<String> menuOptions = new List<string> { "Browse Games", "Interest List", "Recommendations", "Reset interest list", "Exit" };
		var selectedIndex = _menuLogic.CallMenu(_prompt, menuOptions, CurrentIndex, _notification);
		CurrentIndex = selectedIndex;

		switch (selectedIndex)
        {
            case 0: // Browse games
				Console.Clear();
				_browseController.ListGames();
				BrowseMenu();
				break;
			case 1: // See list of games added to interest list
                _interestController.GetGamesOnInterestListWithOptions();
                _interestController.ListInterested();
				CurrentIndex = 0;
				ShowInterestList();
				break;
            case 2: // 
	            _recommendController.GetRecommendedGameWithOptions();
                _recommendController.ListRecommendedGames();
				CurrentIndex = 0;
				RecommendMenu();
				break;
            case 3:
                ClearInterestList();
                MainMenu();
                break;
			case 4: // Exit the application
				Environment.Exit(0);
				break;
		}
    }

	public void BrowseMenu()
	{
        List<string> gamesWithOptions = _browseController.GetGamesOnPageWithOptions();
        
        _notificationController.Changed += OnChange;
        _notificationController.Leave += OnLeave;

        var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, CurrentIndex, _notification);
		CurrentIndex = selectedIndex;

		switch (selectedIndex)
		{
			case 0: // Return to Main Menu
				ReturnToMainMenu();
				break;
			case 1: // Next Page
            case 2: // Previous Page
				Console.Clear();
				_browseController.CheckCurrentPage(selectedIndex);
				BrowseMenu();
				break;
            case 3: // Divider between menu options and interactive objects.
                BrowseMenu();
                break;
            default: // Displayed objects
				_browseController.GetSelectedGameFromBrowseMenu((selectedIndex - 3) + _browseController.GetCurrentPage() - 10);
				BrowseMenu();
				break;
		}
	}

    // Lists all games added to interest list
    public void ShowInterestList()
    {
        List<string> interestListWithOptions = _interestController.GetGamesOnInterestListWithOptions();
        
        _notificationController.Changed += OnChange;
        _notificationController.Leave += OnLeave;

		if (CurrentIndex+1 > interestListWithOptions.Count())
		{
			CurrentIndex--;
		}

		var selectedIndex = _menuLogic.CallMenu(_prompt, interestListWithOptions, CurrentIndex, _notification);
        CurrentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0:
	            _notificationController.OnLeave();
				ReturnToMainMenu(); // Return to main menu
				break;
            case 1:
            case 2:
				Console.Clear(); // NEXT AND PREVIOUS PAGE
				_interestController.CheckCurrentPageAndDisplayInterestList(selectedIndex);
				ShowInterestList();
				break;
            case 3:
                _notificationController.OnLeave();
                _interestController.GetGamesOnPageWithOptions(); //ADD GAMES TO INTEREST LIST
				_interestController.ListNotInterestedOnCurrentPage();
                CurrentIndex = 0;
				InterestMenu();
                break;
			case 4:
                _notificationController.OnLeave();
                _recommendController.GetRecommendedGameWithOptions(); // LOOK FOR RECOMMENDATIONS
				_recommendController.ListRecommendedGames();
				CurrentIndex = 0;
				RecommendMenu();
				break;
			case 5:
                ShowInterestList(); // Line
                break;
			default:
				_interestController.GetSelectedGameFromInterestList((selectedIndex - 6));
				_interestController.ListInterested();
				ShowInterestList();
				break;
		}
	}

    // Lists all games that are NOT on the users interest list
    public void InterestMenu()
    {
        List<string> gamesWithOptions = _interestController.GetGamesOnPageWithOptions();
        
        _notificationController.Changed += OnChange;
        _notificationController.Leave += OnLeave;

		if (CurrentIndex + 1 > gamesWithOptions.Count())
		{
			CurrentIndex--;
		}

		var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, CurrentIndex, _notification);
        CurrentIndex = selectedIndex;

        switch (selectedIndex)
        {
            case 0: // Return to main menu
				_interestController.SetCurrentPage(10);
				_interestController.ListInterested();
                _notificationController.OnLeave();
                ShowInterestList();
                break;
            case 1: // Next Page
            case 2: // Previous Page
                _interestController.CheckCurrentPageAndDisplayGamesNotOnInterestList(selectedIndex);
                InterestMenu();
                break;
            case 3: // Divider between menu options and interactive objects.
                InterestMenu();
                break;
            default: // Displayed objects
				_interestController.GetSelectedGameFromAddToInterestMenu((selectedIndex - 4));
				_interestController.ListNotInterestedOnCurrentPage();
				InterestMenu();
                break;
        }
    }

	public void RecommendMenu()
	{
	    List<string> gamesWithOptions = _recommendController.GetRecommendedGameWithOptions();
	    
	    _notificationController.Changed += OnChange;
	    _notificationController.Leave += OnLeave;

		if (CurrentIndex + 1 > gamesWithOptions.Count())
		{
			CurrentIndex--;
		}

		var selectedIndex = _menuLogic.CallMenu(_prompt, gamesWithOptions, CurrentIndex, _notification);
		CurrentIndex = selectedIndex;
		switch (selectedIndex)
	    {
            case 0: // Return to main menu
                ReturnToMainMenu();
                break;
            case 1: // Divider between menu options and interactive objects.
				RecommendMenu();
				break;
            default:
                _recommendController.GetSelectedGameFromRecommendMenu(selectedIndex-2);
				_recommendController.ListRecommendedGames();
				RecommendMenu();
			    break;
	    }
    }

	// Region !!!
	public void OnLeave(object? source, EventArgs e)
	{
		_notification = "";
	}

    public void OnChange(object? source, ChangeArgs e)
    {
        if (e.method == "add")
        {
            _notification = $"GameID: {e.gameId.ToString()} has been added to the interest list!";
        }

        else if (e.method == "remove")
        {
            _notification = $"GameID: {e.gameId.ToString()} has been removed from the interest list!";
        }
    }

    public void ReturnToMainMenu()
    {
	    _notificationController.OnLeave();
		_browseController.SetCurrentPage(10);
        _interestController.SetCurrentPage(10);
		MainMenu();
    }

	// Mainly for debugging logic error
	public void ClearInterestList()
	{
		Console.Clear();
		for (int i = 1; i <= 100; i++)
		{
			_interestRepo.RemoveGameFromInterest(i);
		}
		_notificationController.OnLeave();
		Console.WriteLine("Interest list cleared");
		Console.WriteLine("Press any KEY to go back to Main menu");
		Console.ReadLine();
	}
}