using NextGaming.Model;

namespace DigitalGameStore.MVC.Controller;

public class NotificationController
{
    public event EventHandler<ChangeArgs>? Changed;
    
    public event EventHandler? Leave;
    
    public virtual void OnChange(int gameId, string method)
    {
        if (Changed != null)
        {
            Changed(this, new ChangeArgs(){ gameId = gameId, method = method});
        }
    }
    
    public virtual void OnLeave()
    {
        if (Leave != null)
        {
            Leave(this, EventArgs.Empty);
        }
    }
}