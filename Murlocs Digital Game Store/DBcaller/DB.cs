using System;
using Microsoft.Data.Sqlite;

namespace DigitalGameStore.DBcaller
{
	public class DB
	{
		public SqliteConnection Connect()
		{
			SqliteConnection _sqliteConnection;
			_sqliteConnection = new SqliteConnection("Data source = Resources/DigitalGameStore.db");
			_sqliteConnection.Open();
			return Connect();
		}
	}
}

