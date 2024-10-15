using Microsoft.Data.Sqlite;
using System.IO;
using System.Text.Json;

namespace WpfLibrary1.Models
{
    public class DBContext
    {
       
        private static readonly string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string databaseFilePath = Path.Combine(documentsPath, "studenten.db");
        private static readonly string connectionString = $"Data Source={databaseFilePath}";
        private const string tableName = "studenten";
        // connection object
        private SqliteConnection connection;

        // constructor
        public DBContext()
        {
            // create table if not exists
            connection = new SqliteConnection(connectionString);
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName} (id INTEGER PRIMARY KEY, name TEXT, age INTEGER)";
                command.ExecuteNonQuery();
            }
        }

        public void AddStudent(string name, int age)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT INTO {tableName} (name, age) VALUES (@name, @age)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@age", age);

                // Execute the command to insert the student
                command.ExecuteNonQuery();
        
            }

        }

        // create method to get all data from table and return JSON string
        public string GetAllData()
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM {tableName}";
                using (var reader = command.ExecuteReader())
                {
                    // Maak een lijst om alle records op te slaan
                    var items = new List<Dictionary<string, object>>();

                    // Lees elk record
                    while (reader.Read())
                    {
                        // Maak een dictionary voor dit specifieke record
                        var record = new Dictionary<string, object>();

                        // Vul de dictionary met de gegevens van het record
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            record[reader.GetName(i)] = reader.GetValue(i);
                        }

                        // Voeg de dictionary toe aan de lijst
                        items.Add(record);
                    }

                    // Serialiseer de lijst van records naar JSON
                    return JsonSerializer.Serialize(items);
                }
            }
        }



        // add destructor
        ~DBContext()
        {
            if (connection.State == System.Data.ConnectionState.Open && connection != null)
            {
                connection.Close();
            }

        }
    }

}
