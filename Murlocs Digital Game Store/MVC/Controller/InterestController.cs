using DigitalGameStore.Interfaces;
using DigitalGameStore.Model;
using DigitalGameStore.Views;

namespace DigitalGameStore.Controller;

public class InterestController {
    private List<string> _allInterest = new();

    private readonly IInterestRepo _interestRepo;
    private readonly InterestView _interestView;

    public InterestController(IInterestRepo interestRepo, InterestView interestView) {
        _interestRepo = interestRepo;
        _interestView = interestView;
    }

    public void AddGameToInterest(int GameID) {
        using (Context db = new Context()) {
            db.Interest.Add(new Interest()
            {
                GameID = GameID
            });
            db.SaveChanges();
        }
    }
}