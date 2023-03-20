using CSharpAcademyBot.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CSharpAcademyBot.Factories
{
    public class AcademyContextFactory : IDesignTimeDbContextFactory<AcademyContext>
    {
        public AcademyContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<AcademyContextFactory>()
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<AcademyContext>();

            // https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/providers?tabs=dotnet-core-cli
            // https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
            var provider = configuration["provider"];
            _ = provider switch
            {
                "sqlserver" => optionsBuilder.UseSqlServer(
                    configuration["ConnectionStrings:SqlConnection"],
                    x => x.MigrationsAssembly("CSharpAcademyBot.SqlServerMigrations")),

                "mysql" => optionsBuilder.UseMySql(
                    configuration["ConnectionStrings:MySqlConnection"],
                    ServerVersion.AutoDetect(configuration["ConnectionStrings:MySqlConnection"]),
                    x => x.MigrationsAssembly("CSharpAcademyBot.MySqlMigrations")),

                _ => throw new Exception($"Unsupported provider: {provider}")
            };

            return new AcademyContext(optionsBuilder.Options);
        }
    }
}
