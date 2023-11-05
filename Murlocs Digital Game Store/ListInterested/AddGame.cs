using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using DigitalGameStore.InterestList;


namespace DigitalGameStore.InterestList
{

    internal class AddGame
    {

        public void Add(int gameId)
        {

            SqliteConnection _sqliteConnection;
            _sqliteConnection = new SqliteConnection($@"Data source = C:\Users\maihe\source\repos\software-design-exam\Murlocs Digital Game Store\Resources\DigitalGameStore.db");
            _sqliteConnection.Open();

            string addSQL = """
                            INSERT INTO Interest (GameID)
                            VALUES (@gameid);
                            """;
            SqliteCommand addCMD = new SqliteCommand(addSQL, _sqliteConnection);
            addCMD.Connection.Open();
            addCMD.Parameters.AddWithValue("@gameid", gameId);
            addCMD.ExecuteNonQuery();
            addCMD.Connection.Close();
        }
    }
}

/* Functionalities:
    1. Display interest_list table
    2. Browse games with up/down arrow
    3. Add games to interest list (trigger BrowseGames())
    4. Delete games
    5. Back to main menu
*/