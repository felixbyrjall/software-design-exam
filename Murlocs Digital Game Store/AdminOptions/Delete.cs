using System.Data;
using Microsoft.Data.Sqlite;
namespace DigitalGameStore.AdminOptions; 

public class Delete {
    
    public void DeleteProduct(string productName) {
        try {
            SqliteConnection _sqliteConnection;
            _sqliteConnection = new SqliteConnection("Data source = Resources/DigitalGameStore.db");
            _sqliteConnection.Open();
            string query = "DELETE FROM Product WHERE Name = @name";
            SqliteCommand deleteCMD = new SqliteCommand(query, _sqliteConnection);
            deleteCMD.Connection.Open();
            deleteCMD.Parameters.AddWithValue("@name", productName);
            var result = deleteCMD.ExecuteNonQuery();
            deleteCMD.Connection.Close();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public void deleteMenu() {
        Console.WriteLine("Welcome to the delete menu");
        Console.WriteLine("Please write the name of the game you want to delete!");
        DeleteProduct(Console.ReadLine());
        Console.WriteLine("Game deleted...");
    }
    
}