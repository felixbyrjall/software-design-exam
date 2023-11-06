using System.Data;
using DigitalGameStore.UI;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.InterestList;

public class DisplayList {

    public void DisplayInterest() {

        List<String> gameSelections = new List<String>();

        SqliteConnection _sqliteConnection;
        _sqliteConnection = new SqliteConnection($@"Data source = Resources/DigitalGameStore.db");
        _sqliteConnection.Open();

        SqliteCommand selectCMD = _sqliteConnection.CreateCommand();

        selectCMD.CommandText = """
                                SELECT Game.Name AS GameName, Game.ID AS GID, Interest.GameID as InterestGID
                                FROM Game
                                JOIN Interest
                                ON GID = InterestGID;
                                """;
        selectCMD.CommandType = CommandType.Text;
        selectCMD.Connection.Open();
        SqliteDataReader myReader = selectCMD.ExecuteReader();
        while (myReader.Read()) {
            Console.WriteLine("Name: " + myReader["GameName"]);
        }



        selectCMD.Connection.Close();

    }



    public void DisplayAllGames()
    {

        List<String> gameSelections = new List<String>();

        SqliteConnection _sqliteConnection;
        _sqliteConnection = new SqliteConnection($@"Data source = Resources/DigitalGameStore.db");
        _sqliteConnection.Open();

        SqliteCommand selectCMD = _sqliteConnection.CreateCommand();

        selectCMD.CommandText = """
                                SELECT Name as GameName                               
                                FROM Game;
                                """;
        selectCMD.CommandType = CommandType.Text;
        selectCMD.Connection.Open();
        SqliteDataReader myReader = selectCMD.ExecuteReader();
        while (myReader.Read())
        {
            string tempString = myReader.GetString("GameName");
            Console.WriteLine(tempString);

            gameSelections.Add(tempString);
        }

        selectCMD.Connection.Close();

        string prompt = "(Use the arrows to select an option)";
        string[] options = gameSelections.ToArray();
        Menu mainMenu = new Menu(prompt, options);

        int selectedIndex = mainMenu.Run();
    }
}