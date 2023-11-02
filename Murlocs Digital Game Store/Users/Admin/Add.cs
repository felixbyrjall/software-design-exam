using System.Data;
using System.Xml.Linq;
using DB;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.Admin; 

public class Add{
    private string? _name;
    private string? _price;
    private string? _date;
    private string? _publisherId;

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
		_name = Console.ReadLine();
		isStringEmpty(_name, nameof(_name));

		Console.WriteLine("Price:");
        _price = Console.ReadLine();
        isInputAnInt(_price, nameof(_price));

        Console.WriteLine("Release date:");
        _date = Console.ReadLine();
        isStringEmpty(_date, nameof(_date));

        Console.WriteLine("Publisher id:");
        _publisherId = Console.ReadLine();
        isInputAnInt(_publisherId, nameof(_publisherId));

        AddProduct(_name,int.Parse(_price),_date,int.Parse(_publisherId));
        
     //  AddProduct("halo", 1, "ead", 1);
        Console.WriteLine("Game added...");

    }

    public void isStringEmpty(string s1, string s2) // Checks if the string inputs of name and release date are null or empty.
    {
		if (string.IsNullOrWhiteSpace(s1))
        {
			Console.WriteLine(s2);
			Console.WriteLine("Invalid input, please try again (Input cannot be empty or a whitespace)");
            switch (s2)
            {
				case nameof(_name):
					Console.WriteLine("Name:");
					_name = Console.ReadLine();
					isStringEmpty(_name, nameof(_name));
					break;
                case nameof(_date):
                    Console.WriteLine("Release date:");
                    _date = Console.ReadLine();
                    isStringEmpty(_date, nameof(_date));
                    break;
            }
        }
	}

	public void isInputAnInt(string s1, string s2) // Checks if the string inputs of price and publisherid are integers and not empty.
	{
		if (!int.TryParse(s1, out _) || string.IsNullOrWhiteSpace(s1))
		{
			Console.WriteLine("Invalid input, please try again (Can only contain a number, and not be empty)");
			switch (s2)
			{
				case nameof(_price):
					Console.WriteLine("Price:");
					_price = Console.ReadLine();
					isInputAnInt(_price, nameof(_price));
					break;
				case nameof(_publisherId):
					Console.WriteLine("Publisher id:");
					_publisherId = Console.ReadLine();
					isInputAnInt(_publisherId, nameof(_price));
					break;
			}
		}
	}
}