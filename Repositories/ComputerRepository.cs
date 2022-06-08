using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Computer> GetAll()
    {
        var computers = new List<Computer>();

        var connection = new SqliteConnection("databaseConfig.ConnectionString");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
           var id = reader.GetInt32(0);
           var ram = reader.GetString(1);
           var processor = reader.GetString(2);
           var computer = new Computer(id, ram, processor);
           computers.Add(computer);
           
        }

        connection.Close();

        return computers;
     
    }
    public void Save(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();

    }

    public Computer GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers WHERE id == $id";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        connection.Close();

        return computer;
    }

    private Computer ReaderToComputer(SqliteDataReader reader)
    {
        var computer = new Computer(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2)
        );

        return computer;
    }

    public bool ExistiByInd(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT FROM Computers WHERE id == $id";
        command.Parameters.AddWithValue("$id", id);

        // var reader = command.ExecuteReader();
        // reader.Read();
        // var result = reader.GetBolean(0);

        bool result = Convert.ToBoolean(command.ExecuteScalar());

        connection.Close();

        return result;

    }

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Computers SET ram = $ram, processor = $processor WHERE id== $id";
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);
        command.Parameters.AddWithValue("$id", computer.Id);

        command.ExecuteNonQuery();
        connection.Close();       

        return GetById(computer.Id);
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELTE FROM Computers WHERE id == $id";
        command.Parameters.AddWithValue("$id", id);
        
        command.ExecuteNonQuery();
        connection.Close();       
    }
}