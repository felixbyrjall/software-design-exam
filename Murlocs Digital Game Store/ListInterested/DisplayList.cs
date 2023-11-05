using System.Data;
using DigitalGameStore.UI;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.InterestList;

public class DisplayList
{

    public void DisplayInterest()
    {

        SqliteConnection _sqliteConnection;
        _sqliteConnection = new SqliteConnection($@"Data source = D:\Users\Jokubas\Documents\Kristiania\Year_2\Software Exam\Murlocs Digital Game Store\Resources\DigitalGameStore.db");
        _sqliteConnection.Open();

        SqliteCommand selectCMD = _sqliteConnection.CreateCommand();

        selectCMD.CommandText = """
                                SELECT Game.Name AS GameName, Game.Game_Id AS GID, Interest.GameID AS InterestGID,  Interest.Interest_Id AS InterestID
                                FROM Game
                                JOIN Interest
                                ON GID = InterestGID;
                                """;
        selectCMD.CommandType = CommandType.Text;
        selectCMD.Connection.Open();
        SqliteDataReader myReader = selectCMD.ExecuteReader();
        while (myReader.Read())
        {


            Console.WriteLine
            ("Name: " + myReader.GetString("GameName") + " Game ID: "
             + myReader.GetInt32("GID") + " Interest ID: " + myReader.GetInt32("InterestID"));
        }

        selectCMD.Connection.Close();
    }

    public void DisplayAllGames()
    {

        List<String> gameSelections = new List<String>();

        SqliteConnection _sqliteConnection;
        _sqliteConnection = new SqliteConnection($@"Data source = C:\Users\maihe\source\repos\software-design-exam\Murlocs Digital Game Store\Resources\DigitalGameStore.db");
        _sqliteConnection.Open();

        SqliteCommand selectCMD = _sqliteConnection.CreateCommand();

        selectCMD.CommandText = """
                                SELECT Game.Name AS GameName, Game.Game_Id AS GID, Interest.GameID AS InterestGID,  Interest.Interest_Id AS InterestID
                                FROM Game
                                JOIN Interest
                                ON GID = InterestGID;
                                """;
        selectCMD.CommandType = CommandType.Text;
        selectCMD.Connection.Open();
        SqliteDataReader myReader = selectCMD.ExecuteReader();
        while (myReader.Read())
        {

            string tempString = myReader.GetString("GameName");
            gameSelections.Add(tempString);
        }

        selectCMD.Connection.Close();

        string prompt = "(Use the arrows to select an option)";
        string[] options = gameSelections.ToArray();
        Menu mainMenu = new Menu(prompt, options);

        int selectedIndex = mainMenu.Run();
    }
}