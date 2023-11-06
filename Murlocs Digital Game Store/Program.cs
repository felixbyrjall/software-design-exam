using DigitalGameStore.Login;

public class Program
{
	public static void Main(string[] args)
	{
		//Console.SetWindowSize(40, 40);

		LoginMenu loginMenu = new LoginMenu();
		loginMenu.LoginOptions();
	}
}