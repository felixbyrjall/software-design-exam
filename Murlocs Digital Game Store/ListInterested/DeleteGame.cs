using Microsoft.Data.Sqlite;

namespace DigitalGameStore.InterestList;

public class DeleteGame
{

    public void Delete(int gameId)
    {

        SqliteConnection _sqliteConnection;
        _sqliteConnection = new SqliteConnection($@"Data source = Resources/DigitalGameStore.db");
        _sqliteConnection.Open();

        string addSQL = """
                        DELETE FROM Interest
                        WHERE GameID = (@gameid);
                        """;
        SqliteCommand addCMD = new SqliteCommand(addSQL, _sqliteConnection);
        addCMD.Connection.Open();
        addCMD.Parameters.AddWithValue("@gameid", gameId);
        addCMD.ExecuteNonQuery();
        addCMD.Connection.Close();
    }
}