using DB;
using DigitalGameStore.DB;

namespace DigitalGameStore.InterestList
{

    internal class AddGame
    {

        public void Add(int gameId)
        {

            Interest newInterest = new()
            {
                GameID = gameId
            };
            
            using Context database = new Context();
            database.Interest.Add(newInterest);
            database.SaveChanges();

            /*SqliteConnection _sqliteConnection;
            _sqliteConnection = new SqliteConnection($@"Data source = Resources\DigitalGameStore.db");
            _sqliteConnection.Open();

            string addSQL = """
                            INSERT INTO Interest (GameID)
                            VALUES (@gameid);
                            """;
            SqliteCommand addCMD = new SqliteCommand(addSQL, _sqliteConnection);
            addCMD.Connection.Open();
            addCMD.Parameters.AddWithValue("@gameid", gameId);
            addCMD.ExecuteNonQuery();
            addCMD.Connection.Close();*/
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