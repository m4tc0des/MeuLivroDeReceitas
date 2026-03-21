using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MySqlConnector;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DatabaseMigration
    {
        public static void Migrate(DataBaseType databaseType, string connectionString, IServiceProvider serviceProvider)
        {
            if (databaseType == DataBaseType.MySql)
            
                EnsureDataBaseCreated_MySQLServer(connectionString);
            
            else
            
                EnsureDataBaseCreated_SQLServer(connectionString);

            MigrationDatabase(serviceProvider);
           
        }

        private static void EnsureDataBaseCreated_MySQLServer(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();

            parameters.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

            if (records.Any() == false)
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }

        private static void EnsureDataBaseCreated_SQLServer(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            
            var databaseName = connectionStringBuilder.InitialCatalog;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();

            parameters.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM sys.databases WHERE NAME = @name", parameters);

            if (records.Any() == false)
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();
        }
    }
}
