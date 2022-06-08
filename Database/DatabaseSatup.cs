namespace LabManager.Database;

using Microsoft.Data.Sqlite;

class DatabaseSatup
{
    public DatabaseSatup()
    {
        CreateComputerTable();
        CreateLabTable();
    }
    
    private void CreateComputerTable()
    {
        var connection = new SqliteConnection("DatabaseConfig.ConnectionString");
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = @"
        CREATE TABLE Computers(
            Id int not null primary key,
            ram varchar(100) not null,
            processor varchar(100) not null
        );
    ";

    command.ExecuteNonQuery();
    connection.Close();

    }

    private void CreateLabTable()
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE Computers(
            Id int not null primary key,
            ram varchar(100) not null,
            processor varchar(100) not null
        );
    ";

    command.ExecuteNonQuery();
    connection.Close();
    }
}