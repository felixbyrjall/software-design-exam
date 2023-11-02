using System.Data;
using System.Xml.Linq;
using DB;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.Admin; 

public class Add{
	private string? name;
    private string? price;
    private string? date;
    private string? publisherId;

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
		name = Console.ReadLine();
		isStringEmpty(name, 1);

		Console.WriteLine("Price:");
        price = Console.ReadLine();
        isInputAnInt(price, 1);

        Console.WriteLine("Release date:");
        date = Console.ReadLine();
        isStringEmpty(date, 2);

        Console.WriteLine("Publisher id:");
        publisherId = Console.ReadLine();
        isInputAnInt(publisherId, 2);

        AddProduct(name,int.Parse(price),date,int.Parse(publisherId));
        
     //  AddProduct("halo", 1, "ead", 1);
        Console.WriteLine("Game added...");

    }

    public void isStringEmpty(string s, int i) // Checks if the string inputs of name and release date are null or empty.
    {
		if (string.IsNullOrWhiteSpace(s))
        {
            Console.WriteLine("Invalid input, please try again (Input cannot be empty or a whitespace)");
            switch (i)
            {
                case 1:
                    Console.WriteLine("Name:");
					name = Console.ReadLine();
					isStringEmpty(name, 1);
					break;
                case 2:
                    Console.WriteLine("Release date:");
                    date = Console.ReadLine();
                    isStringEmpty(date, 2);
                    break;
            }
        }
	}

	public void isInputAnInt(string s, int i) // Checks if the string inputs of name and release date are null or empty.
	{
		if (!int.TryParse(s, out _) || string.IsNullOrWhiteSpace(s))
		{
			Console.WriteLine("Invalid input, please try again (Can only contain a number, and not be empty)");
			switch (i)
			{
				case 1:
					Console.WriteLine("Price:");
					price = Console.ReadLine();
					isInputAnInt(price, 1);
					break;
				case 2:
					Console.WriteLine("Publisher id:");
					publisherId = Console.ReadLine();
					isInputAnInt(publisherId, 2);
					break;
			}
		}
	}
}