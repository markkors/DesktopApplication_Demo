namespace DesktopApplication_Lib.Models
{
    public class DBContext
    {

        private const string databaseFileName = "studenten.db";
        private const string connectionString = "Data Source=studenten.db;Version=3;";
        private const string tableName = "studenten";
        // connection object
        private System.Data.SQLite.SQLiteConnection connection;

        // constructor
        public DBContext()
        {
            // create database if not exists
            if (!System.IO.File.Exists(databaseFileName))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(databaseFileName);
            }
            

            // create table if not exists
            using (connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();
                using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS studenten (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, age INTEGER)";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddStudent(string name, int age)
        {
            // check if connection object is open
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            // add student to database
            using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO studenten (name, age) VALUES (@name, @age)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@age", age);
                command.ExecuteNonQuery();
            }

        }
    }

}
