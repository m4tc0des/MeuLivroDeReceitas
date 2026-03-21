using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAcess;
using MyRecipeBook.Infrastructure.DataAcess.Repositories;
using MyRecipeBook.Infrastructure.Extensions;
using System.Reflection;

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
                AddFluentMigrator_MySql(services, configuration);
            }
            else if (databbaseTypeEnum == DataBaseType.SqlServer)
            {
                AddDbContext_SqlServer(services, configuration);
                AddFluentMigrator_SqlServer(services, configuration);
            }
            AddRepositories(services);
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddDbContext<MyRecipeBookDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
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

        private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddFluentMigratorCore()
                .ConfigureRunner(options =>
                {
                    options
                    .AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
                });
        }

        private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddFluentMigratorCore()
                .ConfigureRunner(options =>
                {
                    options
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
                });
        }

    }
}
