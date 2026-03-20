using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static DataBaseType DatabaseType(this IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            return (DataBaseType)Enum.Parse(typeof(DataBaseType), databaseType!);
        }
        public static string ConnectionString(this IConfiguration configuration)
        {
            var databaseType = configuration.DatabaseType();

            if (databaseType == DataBaseType.MySql)
            {
                return configuration.GetConnectionString("ConnectionMySqlServer")!;
            }
            else
            {
                return configuration.GetConnectionString("ConnectionSqlServer")!;
            }
        }
    }
}
