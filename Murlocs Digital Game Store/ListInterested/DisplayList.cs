using System.Data;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.InterestList;

public class DisplayList
{

    public void DisplayInterest()
    {

        SqliteConnection _sqliteConnection;
        _sqliteConnection = new SqliteConnection($@"Data source = C:\Users\maihe\source\repos\software-design-exam\Murlocs Digital Game Store\Resources\DigitalGameStore.db");
        _sqliteConnection.Open();

        SqliteCommand selectCMD = _sqliteConnection.CreateCommand();

        selectCMD.CommandText = """
                                SELECT Game.Name AS GameName, Game.Game_Id AS GameID, Interest.Interest_Id AS InterestID
                                FROM Game
                                JOIN Interest
                                ON Game.Game_Id = Interest.GameID;
                                """;
        selectCMD.CommandType = CommandType.Text;
        selectCMD.Connection.Open();
        SqliteDataReader myReader = selectCMD.ExecuteReader();
        while (myReader.Read())
        {


            Console.WriteLine
            ("Name: " + myReader.GetString("GameName") + " Game ID: "
             + myReader.GetString("GameID") + " Interest ID: " + myReader.GetInt32("InterestID"));
        }

        selectCMD.Connection.Close();
    }
}