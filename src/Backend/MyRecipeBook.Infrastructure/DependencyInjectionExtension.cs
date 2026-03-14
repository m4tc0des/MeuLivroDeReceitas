using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAcess;
using MyRecipeBook.Infrastructure.DataAcess.Repositories;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            var databbaseTypeEnum = (DataBaseType)Enum.Parse(typeof(DataBaseType), databaseType!);
           

            if (databbaseTypeEnum == DataBaseType.MySql)
            {
                AddDbContext_MySqlServer(services, configuration);
            }
            else if (databbaseTypeEnum == DataBaseType.SqlServer)
            {
                AddDbContext_SqlServer(services, configuration);
            }
            AddRepositories(services);
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionSQLServer");
            services.AddDbContext<MyRecipeBookDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionMySQLServer");
            var ServerVersion = new MySqlServerVersion(new Version(8, 0, 16));
            services.AddDbContext<MyRecipeBookDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion);
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
