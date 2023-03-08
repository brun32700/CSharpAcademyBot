﻿using CSharpAcademyBot.Contexts;
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
            optionsBuilder.UseMySql(configuration["ConnectionStrings:MySqlConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:MySqlConnection"]));

            return new AcademyContext(optionsBuilder.Options);
        }
    }
}