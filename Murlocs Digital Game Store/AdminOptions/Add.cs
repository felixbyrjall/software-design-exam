using System.Data;
using DB;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.AdminOptions; 

public class Add{


    public void AddProduct(string productName, int productPrice, string? releaseDate, int publisherId) {
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
            insertCMD.Parameters.AddWithValue("@publisherid", publisherId);
            insertCMD.ExecuteNonQuery();
            insertCMD.Connection.Close();


            using (SqliteCommand selectCMD = _sqliteConnection.CreateCommand()) {
                selectCMD.CommandText = "SELECT * FROM Product";
                selectCMD.CommandType = CommandType.Text;
                selectCMD.Connection.Open();
                SqliteDataReader myReader = selectCMD.ExecuteReader();
                while (myReader.Read()) {
                    Console.WriteLine
                        ("Product ID: " + myReader["Product_Id"] + " Name: " 
                         +myReader["Name"] + " Price: " + myReader["Price"] + " Publisher:" +myReader["publisherId"]);
                }
                selectCMD.Connection.Close();
                
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            Console.WriteLine("Funker ikke");
            throw;
        }
    }

    public void addMenu() {
       Console.WriteLine("Welcome to the add menu");
        Console.WriteLine("Please fill inn the following information:");
        Console.WriteLine("Name:");
        string name = Console.ReadLine();
        Console.WriteLine("Price:");
        string price = Console.ReadLine();
        Console.WriteLine("Release date");
        string date = Console.ReadLine();
        Console.WriteLine("Publisher id");
        string publisherId = Console.ReadLine();
        AddProduct(name,int.Parse(price),date,int.Parse(publisherId));
        
     //  AddProduct("halo", 1, "ead", 1);
        Console.WriteLine("Game added...");

    }
    
    
    
}