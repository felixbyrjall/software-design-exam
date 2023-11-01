using System.Data;
using DB;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.AdminOptions; 

public class Add {


    public void AddProduct(string productName, int productPrice, string releaseDate, int publisherID) {
        try {
            SqliteConnection _sqliteConnection;
            _sqliteConnection = new SqliteConnection("Data source = Resources/DigitalGameStore.db");
            _sqliteConnection.Open();
            string query =
                "INSERT INTO Product (Name, Price, Date, PublisherID) VALUES (@name, @price, @date, @publisherid)";
            SqliteCommand insertCMD = new SqliteCommand(query, _sqliteConnection);
            insertCMD.Connection.Open();
            insertCMD.Parameters.AddWithValue("@name", productName);
            insertCMD.Parameters.AddWithValue("@price", productPrice);
            insertCMD.Parameters.AddWithValue("@date", releaseDate);
            insertCMD.Parameters.AddWithValue("@publisherid", publisherID);
            var result = insertCMD.ExecuteNonQuery();
            insertCMD.Connection.Close();


            using (SqliteCommand selectCMD = _sqliteConnection.CreateCommand()) {
               _sqliteConnection.Open();
                selectCMD.CommandText = "SELECT * FROM Product";
                selectCMD.CommandType = CommandType.Text;
                SqliteDataReader myReader = selectCMD.ExecuteReader();
                while (myReader.Read()) {
                    Console.WriteLine(myReader["Name"] + " " + myReader["Price"]);
                }
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            Console.WriteLine("Funker ikke");
            throw;
        }
    }
    
    
}