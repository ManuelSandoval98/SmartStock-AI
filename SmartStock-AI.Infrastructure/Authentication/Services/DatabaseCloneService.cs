using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SmartStock_AI.Infrastructure.Authentication.Services;

public class DatabaseCloneService
{
    private readonly IConfiguration _configuration;

    public DatabaseCloneService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void CloneDatabaseFromTemplate(string newDatabaseName)
    {
        // 🔹 Cadena de conexión al servidor de PostgreSQL
        var adminConnectionString = _configuration.GetConnectionString("AdminConnection");

        using var connection = new NpgsqlConnection(adminConnectionString);
        connection.Open();

        // 🔹 Ejecutar el comando SQL para clonar la base de datos
        using var command = connection.CreateCommand();
        command.CommandText = $"CREATE DATABASE \"{newDatabaseName}\" TEMPLATE smartstockai_db;"; // Asegúrate de que smartstockai_db existe como template

        command.ExecuteNonQuery();
    }
}