using NextGaming.Model;

namespace DigitalGameStore.MVC.Controller;

public class NotificationController
{
    public event EventHandler<ChangeArgs>? Changed;
    
    public event EventHandler? Navigated;
    
    public event EventHandler? Cleared;
    
    public virtual void OnChange(int gameId, string method)
    {
        if (Changed != null)
        {
            Changed(this, new ChangeArgs(){ gameId = gameId, method = method});
        }
    }
    
    public virtual void OnNavigate()
    {
        if (Navigated != null)
        {
            Navigated(this, EventArgs.Empty);
        }
    }
    
    public virtual void OnClear()
    {
        if (Cleared != null)
        {
            Cleared(this, EventArgs.Empty);
        }
    }
}