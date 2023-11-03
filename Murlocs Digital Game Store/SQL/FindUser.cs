using System.Data;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.Login;

public class FindUser {
    private static string _username = "";
    private static string _type = "";
    
    public void userSearch(string username, string password) {
        
        SqliteConnection _sqliteConnection;
        _sqliteConnection = new SqliteConnection("Data source = Resources/DigitalGameStore.db");
        _sqliteConnection.Open();

        SqliteCommand selectCMD = _sqliteConnection.CreateCommand();
        selectCMD.CommandText = "SELECT Username, Password, Type FROM Users WHERE Username = @username AND Password = @password";
        selectCMD.CommandType = CommandType.Text;
        selectCMD.Parameters.AddWithValue("@username", username);
        selectCMD.Parameters.AddWithValue("@password", password);
        selectCMD.Connection.Open();
        SqliteDataReader myReader = selectCMD.ExecuteReader();
        while(myReader.Read()) {
            
            _username = myReader.GetString("Username");
            _type = myReader.GetString("Type");
        }
    }

    public string GetUsername() {
        return _username;
    }
    
    public string GetType() {
        return _type;
    }
}
